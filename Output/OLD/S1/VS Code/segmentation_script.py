import pandas as pd
from sklearn.cluster import KMeans
import warnings

# Suppress harmless scikit-learn warnings
warnings.filterwarnings("ignore")

# --- 1. LOAD AND PREPARE DATA ---
print("Loading data...")
df = pd.read_csv('sales_transactions_cleaned.csv')

# Ensure we have a revenue column for the calculations
if 'revenue' not in df.columns:
    df['revenue'] = df['quantity'] * df['price']


# --- 2. TASK 1: CUSTOMER SEGMENTATION ---
print("Engineering features and clustering...")

# Step A: Get the total value of each transaction first
transaction_totals = df.groupby(['customer_id', 'transaction_id'])['revenue'].sum().reset_index()

# Step B: Calculate total_purchases and avg_purchase_value per customer
customer_features = transaction_totals.groupby('customer_id').agg(
    total_purchases=('transaction_id', 'count'),
    avg_purchase_value=('revenue', 'mean')
).reset_index()

# Step C: Apply K-Means Clustering (3 clusters)
# We use random_state=42 to ensure the results are exactly the same every time it runs
kmeans = KMeans(n_clusters=3, random_state=42)
# We add +1 so the labels are 1, 2, and 3 instead of 0, 1, and 2
customer_features['cluster_label'] = kmeans.fit_predict(customer_features[['total_purchases', 'avg_purchase_value']]) + 1

# Merge the cluster labels back to the main transactions dataframe for later use
df = df.merge(customer_features[['customer_id', 'cluster_label']], on='customer_id', how='left')


# --- 3. TASK 2: PRODUCT AFFINITY (Market Basket Analysis) ---
print("Calculating product affinity...")
# The prompt asks to determine the top 3 products frequently purchased together.
# We do this by matching transactions against themselves to find pairs.
basket = df[['transaction_id', 'product_id']]
pairs = basket.merge(basket, on='transaction_id')
# Remove rows where the product is paired with itself
pairs = pairs[pairs['product_id_x'] != pairs['product_id_y']]

# Count how many times Product X and Product Y were bought together
affinity_counts = pairs.groupby(['product_id_x', 'product_id_y']).size().reset_index(name='times_bought_together')
affinity_counts = affinity_counts.sort_values(['product_id_x', 'times_bought_together'], ascending=[True, False])

# Get the top 3 affinities for each product (Useful for the judges/documentation)
top_affinities = affinity_counts.groupby('product_id_x').head(3)


# --- 4. TASK 3: RECOMMENDATION ENGINE ---
print("Generating customer recommendations...")
# Build a list of all products each customer has already bought
customer_history = df.groupby('customer_id')['product_id'].unique().to_dict()

# Find the most popular products in each cluster based on total quantity sold
cluster_popularity = df.groupby(['cluster_label', 'product_id'])['quantity'].sum().reset_index()
cluster_popularity = cluster_popularity.sort_values(['cluster_label', 'quantity'], ascending=[True, False])

# Create a dictionary holding the ranked list of products for each segment
segment_top_products = {
    cluster: cluster_popularity[cluster_popularity['cluster_label'] == cluster]['product_id'].tolist()
    for cluster in [1, 2, 3]
}

# Generate recommendations for each customer
recommendations = []

for _, row in customer_features.iterrows():
    cust_id = row['customer_id']
    cluster = row['cluster_label']
    
    # Get what they already bought
    already_bought = set(customer_history.get(cust_id, []))
    
    # Look at their segment's favorite products
    segment_favorites = segment_top_products[cluster]
    
    # Keep the ones they haven't bought yet
    to_recommend = [prod for prod in segment_favorites if prod not in already_bought]
    
    # Grab the top 3 (pad with 'None' if they somehow bought every single item the bakery sells)
    top_3 = to_recommend[:3]
    while len(top_3) < 3:
        top_3.append('None')
        
    recommendations.append({
        'customer_id': cust_id,
        'cluster_label': cluster,
        'recommended_product_1': top_3[0],
        'recommended_product_2': top_3[1],
        'recommended_product_3': top_3[2]
    })


# --- 5. FORMAT DELIVERABLE AND EXPORT ---
print("Exporting final CSV...")
final_df = pd.DataFrame(recommendations)

# Ensure the columns match the exact requested format
final_df = final_df[['customer_id', 'cluster_label', 'recommended_product_1', 'recommended_product_2', 'recommended_product_3']]

# Export to CSV
final_df.to_csv('Session5_Segmentation_and_Recommendations.csv', index=False)

print("Success! File saved as Session5_Segmentation_and_Recommendations.csv")