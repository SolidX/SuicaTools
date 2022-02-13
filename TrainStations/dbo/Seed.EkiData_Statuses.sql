MERGE INTO EkiData_Statuses T
USING (
    SELECT 0 AS Id, N'運用中' AS Name_JA, 'Active' AS Name_EN
    UNION ALL
    SELECT 1 AS Id, N'運用前' AS Name_JA, 'Future Use' AS Name_EN
    UNION ALL
    SELECT 2 AS Id, N'廃止' AS Name_JA, 'Inactive' AS Name_EN
) S ON T.Id = S.Id
WHEN MATCHED THEN UPDATE SET T.Name_JA = S.Name_JA, T.Name_EN = S.Name_EN
WHEN NOT MATCHED BY TARGET THEN INSERT (Id, Name_JA, Name_EN) VALUES (Id, Name_JA, Name_EN);