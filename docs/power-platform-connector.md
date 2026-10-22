# Power Platform Connector

## Overview

The PensionBridge API can be integrated with Microsoft Power Platform (Power Apps, Power Automate) for building custom applications and workflows.

## Authentication

Use OAuth 2.0 with Entra ID (Azure AD) for authentication:

1. Register an application in Entra ID
2. Configure the API resource
3. Grant appropriate permissions (Application or Delegated)
4. Use the token endpoint to obtain access tokens

## API Endpoints

### Contribution Batches

#### Upload CSV
```
POST /api/contributionbatches
Content-Type: multipart/form-data

Parameters:
- file: CSV file
- employerId: Employer GUID
- schemeId: Scheme GUID
- period: Period string (e.g., "2024-01")
```

#### Get Batch Status
```
GET /api/contributionbatches/{id}
```

### Schemes

#### List Schemes
```
GET /api/schemes
```

#### Get Scheme by Code
```
GET /api/schemes/bycode/{code}
```

## Power Automate Flow Example

### Create a Flow to Process Contributions

1. **Trigger**: When a file is created in OneDrive/SharePoint
2. **Action**: HTTP - POST to PensionBridge API
   - URL: `https://your-api-url/api/contributionbatches`
   - Headers: Authorization (Bearer token)
   - Body: Form data with file and parameters
3. **Condition**: Check response status
4. **Action**: Send email notification with result

## Error Handling

All API responses follow ProblemDetails format:
- 400: Bad Request (validation errors)
- 401: Unauthorized (invalid/missing token)
- 403: Forbidden (insufficient permissions)
- 404: Not Found (resource doesn't exist)
- 500: Internal Server Error

## Security Considerations

- Store secrets in Azure Key Vault
- Use managed identities when possible
- Implement IP restrictions
- Enable audit logging
- Rotate secrets regularly
