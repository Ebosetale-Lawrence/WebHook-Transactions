Explanation (≤300 words)

This service exposes a webhook endpoint to ingest transaction data and persist it in a PostgreSQL database. 
Idempotency is enforced using a unique constraint on the TransactionId field, ensuring duplicate events from the provider do not create multiple records.

Upon receiving a valid transaction, the system performs a derived computation (a 2% transaction fee) 
and stores the resulting net amount in a separate DerivedRecords table. This separation keeps raw data
immutable while allowing flexibility for derived data evolution.

The implementation prioritizes simplicity, using ASP.NET Core with Entity Framework for persistence.
The endpoint is synchronous and returns a success response for both new and duplicate transactions to maintain webhook reliability.

Assumptions (max 3)
1. TransactionId is globally unique from provider
2. Webhook retries may happen (hence idempotency needed)
3. Only one currency per transaction
