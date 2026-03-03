import pandas as pd
from statsmodels.tsa.arima.model import ARIMA
from sklearn.metrics import mean_absolute_error
import warnings

# import data
df = pd.read_csv('sales_transactions_cleaned.csv')

# calculate revenue
df['Revenue'] = df['quantity'] * df['price']

# make sure date is recognized as datetime
df['date'] = pd.to_datetime(df['date'], format="%d/%m/%Y %H:%M").dt.date

daily_sales = df.groupby('date')['Revenue'].sum().reset_index()

daily_sales.set_index('date', inplace=True)
daily_sales.index = pd.to_datetime(daily_sales.index)

daily_sales = daily_sales.asfreq('D').fillna(0)

train_data = daily_sales.iloc[:-30]
test_data = daily_sales.iloc[-30:]

eval_model = ARIMA(train_data['Revenue'], order=(1,1,1))
eval_results = eval_model.fit()

predictions = eval_results.forecast(steps=30)
mae = mean_absolute_error(test_data['Revenue'], predictions)
print(f"Model MAE: {mae:.2f}")

final_model = ARIMA(daily_sales['Revenue'], order=(1,1,1))
final_results = final_model.fit()

future_forecast = final_results.forecast(steps=30)

forecast_df = future_forecast.reset_index()
forecast_df.columns = ['Date', 'Predicted_Sales']

forecast_df['Date'] = forecast_df['Date'].dt.strftime('%Y-%m-%d')

forecast_df['Predicted_Sales'] = forecast_df['Predicted_Sales'].round(2)

forecast_df.to_csv("Session1_SalesForecast.csv", index=False)
print("Forecast successfully exported")