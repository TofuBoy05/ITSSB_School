import pandas as pd

print("Loading data...")
# Load the cleaned dataset
df = pd.read_csv('sales_transactions_cleaned.csv')

# Ensure date is recognized as a datetime object
df['date'] = pd.to_datetime(df['date'], format='%d/%m/%Y %H:%M')

# Calculate the Revenue per row if it's not already there
if 'revenue' not in df.columns:
    df['revenue'] = df['quantity'] * df['price']

# --- Step 0: Figure out the time span in months ---
# To calculate "transactions per month", we need to know how many months the dataset covers.
# We take the total number of days between the first and last sale, and divide by 30.44 (average days in a month)
total_days = (df['date'].max() - df['date'].min()).days
total_months = total_days / 30.44

# Prevent division by zero just in case the dataset only spans exactly 1 day
if total_months < 1:
    total_months = 1

# --- Step 1 & 2: Calculate APV and Frequency ---
print("Calculating customer metrics...")
# Group the data by customer to get their total revenue and unique number of transactions
customer_stats = df.groupby('customer_id').agg(
    total_revenue=('revenue', 'sum'),
    total_transactions=('transaction_id', 'nunique')
).reset_index()

# Task 1: Average Purchase Value (Total Revenue / Total Transactions)
customer_stats['avg_purchase_value'] = customer_stats['total_revenue'] / customer_stats['total_transactions']

# Task 2: Purchase Frequency (Total Transactions / Total Months)
customer_stats['purchase_frequency'] = customer_stats['total_transactions'] / total_months

# --- Step 3: Calculate CLTV ---
print("Calculating CLTV...")
# Task 3: CLTV = Average Purchase Value * Purchase Frequency * 36
customer_stats['cltv'] = customer_stats['avg_purchase_value'] * customer_stats['purchase_frequency'] * 36

# --- Step 4: Format and Export ---
print("Formatting and exporting...")
# Round CLTV to 2 decimal places as requested
customer_stats['cltv'] = customer_stats['cltv'].round(2)

# Ensure the columns are exactly what the deliverable asks for
final_df = customer_stats[['customer_id', 'cltv']]

# Export to CSV
final_df.to_csv('Session1_CLTV.csv', index=False)

print("Success! File saved as Session1_CLTV.csv")