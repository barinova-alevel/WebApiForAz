using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SFMB.DAL.Migrations
{
    /// <inheritdoc />
    public partial class SeedOperationsAndOperationTypesFromJson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // First, create a default user for the seed data if it doesn't exist
            // We'll use a raw SQL approach to ensure idempotency
            migrationBuilder.Sql(@"
                DO $$
                DECLARE
                    v_user_id text;
                BEGIN
                    -- Check if user exists
                    SELECT ""Id"" INTO v_user_id 
                    FROM ""AspNetUsers"" 
                    WHERE ""Email"" = 'user1@sfmb.com' 
                    LIMIT 1;
                    
                    -- If user doesn't exist, create one for seed data
                    IF v_user_id IS NULL THEN
                        v_user_id := gen_random_uuid()::text;
                        
                        INSERT INTO ""AspNetUsers"" (
                            ""Id"", 
                            ""UserName"", 
                            ""NormalizedUserName"", 
                            ""Email"", 
                            ""NormalizedEmail"", 
                            ""EmailConfirmed"", 
                            ""PasswordHash"",
                            ""SecurityStamp"",
                            ""ConcurrencyStamp"",
                            ""PhoneNumberConfirmed"",
                            ""TwoFactorEnabled"",
                            ""LockoutEnabled"",
                            ""AccessFailedCount"",
                            ""FirstName"",
                            ""LastName"",
                            ""CreatedAt""
                        ) VALUES (
                            v_user_id,
                            'user1@sfmb.com',
                            'USER1@SFMB.COM',
                            'user1@sfmb.com',
                            'USER1@SFMB.COM',
                            true,
                            'AQAAAAIAAYagAAAAEKzX8z3xF3hn7x9kYYrZ5mH5rS5Ny5Jt5L3h3J3d3z3x3v3b3n3m3k3j3h3g3f3d3c3b3a==',
                            gen_random_uuid()::text,
                            gen_random_uuid()::text,
                            false,
                            false,
                            true,
                            0,
                            'John',
                            'Doe',
                            NOW()
                        );
                    END IF;
                    
                    -- Now seed OperationTypes
                    INSERT INTO ""OperationTypes"" (""OperationTypeId"", ""Name"", ""Description"", ""IsIncome"", ""UserId"")
                    SELECT 1, 'Salary', 'Monthly job income', true, v_user_id
                    WHERE NOT EXISTS (SELECT 1 FROM ""OperationTypes"" WHERE ""OperationTypeId"" = 1);
                    
                    INSERT INTO ""OperationTypes"" (""OperationTypeId"", ""Name"", ""Description"", ""IsIncome"", ""UserId"")
                    SELECT 2, 'Freelance', 'Freelance or side project payments', true, v_user_id
                    WHERE NOT EXISTS (SELECT 1 FROM ""OperationTypes"" WHERE ""OperationTypeId"" = 2);
                    
                    INSERT INTO ""OperationTypes"" (""OperationTypeId"", ""Name"", ""Description"", ""IsIncome"", ""UserId"")
                    SELECT 3, 'Groceries', 'Daily food and household shopping', false, v_user_id
                    WHERE NOT EXISTS (SELECT 1 FROM ""OperationTypes"" WHERE ""OperationTypeId"" = 3);
                    
                    INSERT INTO ""OperationTypes"" (""OperationTypeId"", ""Name"", ""Description"", ""IsIncome"", ""UserId"")
                    SELECT 4, 'Utilities', 'Electricity, water, gas, etc.', false, v_user_id
                    WHERE NOT EXISTS (SELECT 1 FROM ""OperationTypes"" WHERE ""OperationTypeId"" = 4);
                    
                    INSERT INTO ""OperationTypes"" (""OperationTypeId"", ""Name"", ""Description"", ""IsIncome"", ""UserId"")
                    SELECT 5, 'Entertainment', 'Movies, games, subscriptions', false, v_user_id
                    WHERE NOT EXISTS (SELECT 1 FROM ""OperationTypes"" WHERE ""OperationTypeId"" = 5);
                    
                    INSERT INTO ""OperationTypes"" (""OperationTypeId"", ""Name"", ""Description"", ""IsIncome"", ""UserId"")
                    SELECT 6, 'Investment Return', 'Dividends or capital gains', true, v_user_id
                    WHERE NOT EXISTS (SELECT 1 FROM ""OperationTypes"" WHERE ""OperationTypeId"" = 6);
                    
                    INSERT INTO ""OperationTypes"" (""OperationTypeId"", ""Name"", ""Description"", ""IsIncome"", ""UserId"")
                    SELECT 7, 'Rent', 'Monthly house or apartment rent', false, v_user_id
                    WHERE NOT EXISTS (SELECT 1 FROM ""OperationTypes"" WHERE ""OperationTypeId"" = 7);
                    
                    -- Now seed Operations
                    INSERT INTO ""Operations"" (""OperationId"", ""Date"", ""Amount"", ""Note"", ""OperationTypeId"", ""UserId"")
                    SELECT 1, '2025-05-01'::date, 40000, 'Monthly job income', 1, v_user_id
                    WHERE NOT EXISTS (SELECT 1 FROM ""Operations"" WHERE ""OperationId"" = 1);
                    
                    INSERT INTO ""Operations"" (""OperationId"", ""Date"", ""Amount"", ""Note"", ""OperationTypeId"", ""UserId"")
                    SELECT 2, '2025-05-01'::date, 220.50, 'food', 3, v_user_id
                    WHERE NOT EXISTS (SELECT 1 FROM ""Operations"" WHERE ""OperationId"" = 2);
                    
                    INSERT INTO ""Operations"" (""OperationId"", ""Date"", ""Amount"", ""Note"", ""OperationTypeId"", ""UserId"")
                    SELECT 3, '2025-05-02'::date, 554.70, 'electricity, water', 4, v_user_id
                    WHERE NOT EXISTS (SELECT 1 FROM ""Operations"" WHERE ""OperationId"" = 3);
                    
                    INSERT INTO ""Operations"" (""OperationId"", ""Date"", ""Amount"", ""Note"", ""OperationTypeId"", ""UserId"")
                    SELECT 4, '2025-05-02'::date, 358, 'netflix', 5, v_user_id
                    WHERE NOT EXISTS (SELECT 1 FROM ""Operations"" WHERE ""OperationId"" = 4);
                    
                    INSERT INTO ""Operations"" (""OperationId"", ""Date"", ""Amount"", ""Note"", ""OperationTypeId"", ""UserId"")
                    SELECT 5, '2025-05-02'::date, 16350, 'apartment rent', 7, v_user_id
                    WHERE NOT EXISTS (SELECT 1 FROM ""Operations"" WHERE ""OperationId"" = 5);
                    
                    INSERT INTO ""Operations"" (""OperationId"", ""Date"", ""Amount"", ""Note"", ""OperationTypeId"", ""UserId"")
                    SELECT 6, '2025-05-03'::date, 1020.80, 'market', 3, v_user_id
                    WHERE NOT EXISTS (SELECT 1 FROM ""Operations"" WHERE ""OperationId"" = 6);
                    
                    INSERT INTO ""Operations"" (""OperationId"", ""Date"", ""Amount"", ""Note"", ""OperationTypeId"", ""UserId"")
                    SELECT 7, '2025-05-03'::date, 4000, 'consulting', 2, v_user_id
                    WHERE NOT EXISTS (SELECT 1 FROM ""Operations"" WHERE ""OperationId"" = 7);
                    
                    INSERT INTO ""Operations"" (""OperationId"", ""Date"", ""Amount"", ""Note"", ""OperationTypeId"", ""UserId"")
                    SELECT 8, '2025-05-04'::date, 820, '', 5, v_user_id
                    WHERE NOT EXISTS (SELECT 1 FROM ""Operations"" WHERE ""OperationId"" = 8);
                    
                    INSERT INTO ""Operations"" (""OperationId"", ""Date"", ""Amount"", ""Note"", ""OperationTypeId"", ""UserId"")
                    SELECT 9, '2025-05-05'::date, 325, 'pizza', 3, v_user_id
                    WHERE NOT EXISTS (SELECT 1 FROM ""Operations"" WHERE ""OperationId"" = 9);
                    
                    INSERT INTO ""Operations"" (""OperationId"", ""Date"", ""Amount"", ""Note"", ""OperationTypeId"", ""UserId"")
                    SELECT 10, '2025-05-05'::date, 120, 'deposit percentage', 6, v_user_id
                    WHERE NOT EXISTS (SELECT 1 FROM ""Operations"" WHERE ""OperationId"" = 10);
                    
                    INSERT INTO ""Operations"" (""OperationId"", ""Date"", ""Amount"", ""Note"", ""OperationTypeId"", ""UserId"")
                    SELECT 11, '2025-05-06'::date, 780, 'food delivery', 3, v_user_id
                    WHERE NOT EXISTS (SELECT 1 FROM ""Operations"" WHERE ""OperationId"" = 11);
                    
                    INSERT INTO ""Operations"" (""OperationId"", ""Date"", ""Amount"", ""Note"", ""OperationTypeId"", ""UserId"")
                    SELECT 12, '2025-05-06'::date, 535, 'lunch', 3, v_user_id
                    WHERE NOT EXISTS (SELECT 1 FROM ""Operations"" WHERE ""OperationId"" = 12);
                    
                    INSERT INTO ""Operations"" (""OperationId"", ""Date"", ""Amount"", ""Note"", ""OperationTypeId"", ""UserId"")
                    SELECT 13, '2025-05-08'::date, 370, 'market', 3, v_user_id
                    WHERE NOT EXISTS (SELECT 1 FROM ""Operations"" WHERE ""OperationId"" = 13);
                    
                    INSERT INTO ""Operations"" (""OperationId"", ""Date"", ""Amount"", ""Note"", ""OperationTypeId"", ""UserId"")
                    SELECT 14, '2025-05-08'::date, 400, 'bike rent', 5, v_user_id
                    WHERE NOT EXISTS (SELECT 1 FROM ""Operations"" WHERE ""OperationId"" = 14);
                    
                    INSERT INTO ""Operations"" (""OperationId"", ""Date"", ""Amount"", ""Note"", ""OperationTypeId"", ""UserId"")
                    SELECT 15, '2025-05-09'::date, 520, 'street food', 3, v_user_id
                    WHERE NOT EXISTS (SELECT 1 FROM ""Operations"" WHERE ""OperationId"" = 15);
                    
                    INSERT INTO ""Operations"" (""OperationId"", ""Date"", ""Amount"", ""Note"", ""OperationTypeId"", ""UserId"")
                    SELECT 16, '2025-05-10'::date, 250.72, 'gas', 4, v_user_id
                    WHERE NOT EXISTS (SELECT 1 FROM ""Operations"" WHERE ""OperationId"" = 16);
                    
                    INSERT INTO ""Operations"" (""OperationId"", ""Date"", ""Amount"", ""Note"", ""OperationTypeId"", ""UserId"")
                    SELECT 17, '2025-05-10'::date, 10, 'matches', 3, v_user_id
                    WHERE NOT EXISTS (SELECT 1 FROM ""Operations"" WHERE ""OperationId"" = 17);
                    
                    INSERT INTO ""Operations"" (""OperationId"", ""Date"", ""Amount"", ""Note"", ""OperationTypeId"", ""UserId"")
                    SELECT 18, '2025-05-10'::date, 100, 'market', 3, v_user_id
                    WHERE NOT EXISTS (SELECT 1 FROM ""Operations"" WHERE ""OperationId"" = 18);
                    
                    INSERT INTO ""Operations"" (""OperationId"", ""Date"", ""Amount"", ""Note"", ""OperationTypeId"", ""UserId"")
                    SELECT 19, '2025-05-10'::date, 155, 'youtube subscription', 5, v_user_id
                    WHERE NOT EXISTS (SELECT 1 FROM ""Operations"" WHERE ""OperationId"" = 19);
                    
                    INSERT INTO ""Operations"" (""OperationId"", ""Date"", ""Amount"", ""Note"", ""OperationTypeId"", ""UserId"")
                    SELECT 20, '2025-05-10'::date, 2500, 'bonus', 1, v_user_id
                    WHERE NOT EXISTS (SELECT 1 FROM ""Operations"" WHERE ""OperationId"" = 20);
                END $$;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Delete the seeded data
            migrationBuilder.Sql(@"
                DELETE FROM ""Operations"" WHERE ""OperationId"" BETWEEN 1 AND 20;
                DELETE FROM ""OperationTypes"" WHERE ""OperationTypeId"" BETWEEN 1 AND 7;
            ");
        }
    }
}
