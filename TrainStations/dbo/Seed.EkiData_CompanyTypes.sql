MERGE INTO EkiData_CompanyTypes T
USING (
    SELECT 0 AS Id, N'その他' AS Name_JA, 'Other' AS Name_EN
    UNION ALL
    SELECT 1 AS Id, N'JR' AS Name_JA, 'Japan Railways Group' AS Name_EN
    UNION ALL
    SELECT 2 AS Id, N'大手私鉄' AS Name_JA, 'Major Private Railway' AS Name_EN
    UNION ALL
    SELECT 3 AS Id, N'準大手私鉄' AS Name_JA, 'Semi-major Private Railway' AS Name_EN
) S ON T.Id = S.Id
WHEN MATCHED THEN UPDATE SET T.Name_JA = S.Name_JA, T.Name_EN = S.Name_EN
WHEN NOT MATCHED BY TARGET THEN INSERT (Id, Name_JA, Name_EN) VALUES (Id, Name_JA, Name_EN);