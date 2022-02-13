/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

MERGE INTO EkiData_LineTypes T
USING (
    SELECT 0 AS Id, N'その他' AS Name_JA, 'Other' AS Name_EN
    UNION ALL
    SELECT 1 AS Id, N'新幹線' AS Name_JA, 'Shinkansen' AS Name_EN
    UNION ALL
    SELECT 2 AS Id, N'一般' AS Name_JA, 'General' AS Name_EN
    UNION ALL
    SELECT 3 AS Id, N'地下鉄' AS Name_JA, 'Subway' AS Name_EN
    UNION ALL
    SELECT 4 AS Id, N'市 AS Name_JA電・路面電車', 'Trams' AS Name_EN
    UNION ALL
    SELECT 5 AS Id, N'モノレー AS Name_JAル・新交通', 'Monorail / People Mover' AS Name_EN
) S ON T.Id = S.Id
WHEN MATCHED THEN UPDATE SET T.Name_JA = S.Name_JA, T.Name_EN = S.Name_EN
WHEN NOT MATCHED BY TARGET THEN INSERT (Id, Name_JA, Name_EN) VALUES (T.Id, T.Name_JA, T.Name_EN);