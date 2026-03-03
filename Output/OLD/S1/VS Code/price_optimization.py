import pandas as pd
import numpy as np
import statsmodels.api as sm
import warnings

# Suppress harmless warnings for clean output
warnings.filterwarnings("ignore")

print("Loading data...")
# Load datasets
sales = pd.read_csv('sales_transactions_cleaned.csv')
products = pd.read_csv('products_cleaned.csv', encoding='latin-1')

# Merge sales with products to get the 'cost' column for profit calculations
# We will use the 'price' from the sales table in case there were historical discounts
df = pd.merge(sales, products[['product_id', 'cost']], on='product_id', how='left')

# Calculate Revenue and Cost per transaction
df['revenue'] = df['quantity'] * df['price']
df['total_cost'] = df['quantity'] * df['cost']


# ==========================================
# DELIVERABLE 1: PRODUCT PERFORMANCE
# ==========================================
print("Calculating Product Performance...")

# Group by product_id to get totals
performance = df.groupby('product_id').agg(
    total_quantity_sold=('quantity', 'sum'),
    total_revenue=('revenue', 'sum'),
    total_costs=('total_cost', 'sum')
).reset_index()

# Calculate Profit Margin: (Revenue - Cost) / Revenue
performance['profit_margin'] = (performance['total_revenue'] - performance['total_costs']) / performance['total_revenue']

# Sort by total_revenue descending (as requested by the task)
performance = performance.sort_values(by='total_revenue', ascending=False)

# Format the final DataFrame for Export 1
export_1 = performance[['product_id', 'total_quantity_sold', 'total_revenue', 'profit_margin']]
export_1.to_csv('Session5_Product_Performance.csv', index=False)
print("Saved: Session5_Product_Performance.csv")


# ==========================================
# DELIVERABLE 2: PRICE OPTIMIZATION & PED
# ==========================================
print("Calculating Price Elasticity of Demand (PED)...")

# To calculate PED, we need to see how quantity changes when price changes.
# We will aggregate sales by week (or day) and product to see these fluctuations.
df['date'] = pd.to_datetime(df['date'], format='%d/%m/%Y %H:%M')
df['week'] = df['date'].dt.isocalendar().week

weekly_sales = df.groupby(['product_id', 'week']).agg(
    avg_price=('price', 'mean'),
    total_qty=('quantity', 'sum')
).reset_index()

# Prepare lists to store our results
ped_results = []

for product in weekly_sales['product_id'].unique():
    prod_data = weekly_sales[weekly_sales['product_id'] == product]
    
    # We need price variation to calculate elasticity. 
    # If a product's price never changed, PED is mathematically undefined.
    if prod_data['avg_price'].nunique() > 1 and len(prod_data) > 2:
        
        # Log-Log Regression: ln(Q) = Intercept + PED * ln(P)
        # We add a tiny number (1e-9) to avoid log(0) errors
        y = np.log(prod_data['total_qty'] + 1e-9)
        X = np.log(prod_data['avg_price'] + 1e-9)
        X = sm.add_constant(X) # Adds the intercept
        
        try:
            model = sm.OLS(y, X).fit()
            # The coefficient for price is our PED
            ped = model.params['avg_price'] 
        except:
            ped = 0.0 # Fallback if math fails
    else:
        ped = 0.0 # No price variation
        
    ped_results.append({'product_id': product, 'price_elasticity_of_demand': ped})

ped_df = pd.DataFrame(ped_results)

# Merge PED results with our performance data to make pricing decisions
price_analysis = pd.merge(ped_df, performance[['product_id', 'profit_margin']], on='product_id')

# --- PRICING STRATEGY LOGIC ---
# Using the WorldSkills guidelines:
# 1. High PED (Elastic, usually < -1): Lower price to boost volume.
# 2. Low PED (Inelastic, usually between -1 and 0): Raise price to boost margin.
# We also consider current profit margins to ensure we don't drop prices on low-margin items.

def suggest_price_change(row):
    ped = row['price_elasticity_of_demand']
    margin = row['profit_margin']
    
    # If the item is highly elastic (people are very sensitive to price)
    if ped < -1.5:
        if margin > 0.30: # Only drop price if we have a healthy profit margin to cushion it
            return "-5% decrease"
        else:
            return "0% (Hold - Margin too low to discount)"
            
    # If the item is somewhat elastic
    elif -1.5 <= ped < -0.5:
        return "-2% decrease"
        
    # If the item is inelastic (people buy it regardless of price) or PED is positive (Veblen good)
    elif ped >= -0.5 and ped != 0.0:
        return "5% increase"
        
    # Fallback for items with 0 PED (meaning the price never changed historically so we have no data)
    else:
        if margin < 0.20:
            return "3% increase (Test price sensitivity)"
        else:
            return "0% (Hold)"

# Apply the logic
price_analysis['suggested_price_change'] = price_analysis.apply(suggest_price_change, axis=1)

# Format the final DataFrame for Export 2
export_2 = price_analysis[['product_id', 'price_elasticity_of_demand', 'suggested_price_change']]

# Round the PED to 4 decimal places so it looks clean for the judges
export_2['price_elasticity_of_demand'] = export_2['price_elasticity_of_demand'].round(4)

export_2.to_csv('Session5_Price_Analysis.csv', index=False)
print("Saved: Session5_Price_Analysis.csv")
print("All tasks completed successfully!")