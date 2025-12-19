# Seed Data Migration - Operations and OperationTypes

## Overview
This document describes the migration created to seed the database with Operations and OperationTypes data from the JSON files.

## Migration Details

**Migration Name**: `20251219223500_SeedOperationsAndOperationTypesFromJson`

**Purpose**: Populate the Operations and OperationTypes tables with initial data from:
- `operationTypes.json` (7 operation types)
- `operations.json` (20 operations)

## Implementation Approach

The migration uses PostgreSQL PL/pgSQL to ensure idempotent execution with the following logic:

1. **User Creation**:
   - Checks if user `user1@sfmb.com` exists
   - If not, creates the user with a generated UUID
   - This ensures the seed data has a valid UserId (required field)

2. **OperationType Seeding**:
   - Inserts 7 operation types (Salary, Freelance, Groceries, Utilities, Entertainment, Investment Return, Rent)
   - Each insert uses `WHERE NOT EXISTS` to prevent duplicates
   - All operation types are assigned to the created/existing user

3. **Operation Seeding**:
   - Inserts 20 operations spanning dates from 2025-05-01 to 2025-05-10
   - Each insert uses `WHERE NOT EXISTS` to prevent duplicates
   - All operations are assigned to the created/existing user
   - Operations reference the seeded OperationTypes by their IDs

## Key Features

- **Idempotent**: Can be run multiple times without creating duplicate data
- **User-aware**: Properly associates data with a user (required since AddIdentityAndUserOwnership migration)
- **Atomic**: All insertions happen within a single transaction
- **Reversible**: Down migration cleanly removes all seeded data

## Data Summary

### OperationTypes (7 records)
- 3 Income types: Salary, Freelance, Investment Return
- 4 Expense types: Groceries, Utilities, Entertainment, Rent

### Operations (20 records)
- Total Income: 46,620 (operations: 1, 7, 10, 20)
- Total Expenses: 22,203.72 (operations: 2, 3, 4, 5, 6, 8, 9, 11, 12, 13, 14, 15, 16, 17, 18, 19)
- Date Range: 2025-05-01 to 2025-05-10

## How to Apply

To apply this migration to your database:

```bash
cd SFMB.DAL
dotnet ef database update --startup-project ../WebApiForAz
```

To rollback:

```bash
cd SFMB.DAL
dotnet ef database update AddIdentityAndUserOwnership --startup-project ../WebApiForAz
```

## Verification

After applying the migration, you can verify the data:

```sql
-- Check OperationTypes
SELECT * FROM "OperationTypes";

-- Check Operations
SELECT * FROM "Operations";

-- Check the user
SELECT * FROM "AspNetUsers" WHERE "Email" = 'user1@sfmb.com';
```

## Notes

- The migration creates a user if one doesn't exist, but the `DbSeeder` class in the application also creates default users at runtime
- If the DbSeeder runs first, this migration will use the existing user
- The password hash in the migration is a placeholder; the DbSeeder will set the actual password "User123!" when it creates the user
- The seed data is assigned to `user1@sfmb.com` to ensure proper ownership in a multi-tenant system
