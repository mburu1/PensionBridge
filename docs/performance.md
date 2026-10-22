# Performance Documentation

## Database Indexes

The following indexes are configured for optimal query performance:

- **Batches**: Index on Status, IdempotencyKey (unique)
- **Members**: Unique index on {SchemeId, MemberNumber}
- **OutboxMessages**: Index on Status, IdempotencyKey (unique)
- **ReconciliationResults**: Index on BatchId
- **ExceptionCases**: Index on BatchId

## Performance Considerations

### CSV Ingestion
- Large files should be processed in a streaming manner to avoid memory issues
- Maximum file size limit: 100MB (configurable)
- Row count limit: 10,000 rows per batch (configurable)

### Database Operations
- Use batch processing for bulk inserts
- Configure connection pooling for high-throughput scenarios
- Consider read replicas for reporting queries

### Business Central Integration
- Implement exponential backoff for rate-limited API calls
- Cache frequently accessed scheme and member data
- Use async/await for all external API calls

## Tuning Recommendations

### SQL Server
1. Enable Query Store for performance monitoring
2. Configure MAXDOP based on CPU cores
3. Set appropriate memory limits
4. Monitor deadlocks and implement retry logic

### Application
1. Use Response Caching for static API responses
2. Implement rate limiting per client
3. Monitor application metrics (requests, latency, errors)
