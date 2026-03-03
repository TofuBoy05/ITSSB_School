import pandas as pd

print("Loading data...")
# Load the customers dataset and the CLTV dataset you just created
try:
    customers = pd.read_csv('customers_cleaned.csv')
except UnicodeDecodeError:
    # Adding the latin-1 fallback just in case the French characters pop up here too!
    customers = pd.read_csv('customers_cleaned.csv', encoding='latin-1')

cltv_df = pd.read_csv('Session1_CLTV.csv')

# Merge them together so every customer has their churn status and CLTV in one row
df = pd.merge(customers, cltv_df, on='customer_id', how='inner')

# --- STEP 1: IDENTIFY CHURNED CUSTOMERS ---
# IMPORTANT: Change 'is_churned' to the exact name of the churn column in your CSV
churn_column = 'churned' 

# This clever block standardizes the column so the math works perfectly whether 
# your data says "Yes/No", "True/False", or "1/0"
if df[churn_column].dtype == object:
    df['churn_flag'] = df[churn_column].astype(str).str.lower().isin(['yes', 'true', 'churned', '1'])
else:
    df['churn_flag'] = df[churn_column] == 1  # Treats 1 or True as churned

print("Calculating metrics...")

# --- STEP 2: CALCULATE CHURN RATE ---
total_customers = len(df)
churned_customers = df['churn_flag'].sum()

# Calculate the percentage and round to 2 decimal places
churn_rate = round((churned_customers / total_customers) * 100, 2)

# --- STEP 3: CALCULATE AVERAGE CLTV ---
# We filter the dataframe by our flag and take the mean of the cltv column
avg_cltv_churned = round(df[df['churn_flag'] == True]['cltv'].mean(), 2)
avg_cltv_active = round(df[df['churn_flag'] == False]['cltv'].mean(), 2)

# --- STEP 4: FORMAT AND EXPORT ---
print("Formatting and exporting...")

# Create a new dataframe with exactly one row containing our 3 metrics
final_df = pd.DataFrame([{
    'churn_rate': churn_rate,
    'avg_cltv_churned': avg_cltv_churned,
    'avg_cltv_active': avg_cltv_active
}])

# Export to CSV without the index column
final_df.to_csv('Session1_Churn_Analysis.csv', index=False)

print("Success! File saved as Session1_Churn_Analysis.csv")