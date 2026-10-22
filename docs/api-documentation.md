# PensionBridge API Documentation

## Base URL
```
https://your-api-domain/api
```

## Authentication
Use JWT Bearer tokens obtained from Entra ID.

```
Authorization: Bearer {access_token}
```

## Endpoints

### Health Check
```
GET /health/live
GET /health/ready
```

### Contribution Batches
```
POST /contributionbatches
- Upload CSV file with employer, scheme, and period parameters
- Returns batch ID for tracking

GET /contributionbatches/{id}
- Get batch status and details
```

### Schemes
```
GET /schemes
- List all schemes

GET /schemes/bycode/{code}
- Get scheme by code

POST /schemes
- Create new scheme (admin only)

PUT /schemes/{id}
- Update scheme (admin only)
```

### Reconciliation
```
POST /reconciliation/batches/{id}
- Run reconciliation on a batch
- Returns reconciliation results
```

## Error Responses

All errors follow RFC 7807 ProblemDetails format:

```json
{
  "type": "https://example.com/probs/validation",
  "title": "Validation Error",
  "status": 400,
  "detail": "One or more validation errors occurred",
  "errors": {
    "period": ["Period is required"]
  }
}
```

## OpenAPI/Swagger
Development environment: `/openapi/v1.json`
