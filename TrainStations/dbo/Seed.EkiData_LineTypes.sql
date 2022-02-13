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
    SELECT 4 AS Id, N'市電・路面電車' AS Name_JA, 'Trams' AS Name_EN
    UNION ALL
    SELECT 5 AS Id, N'モノレール・新交通' AS Name_JA, 'Monorail / People Mover' AS Name_EN
) S ON T.Id = S.Id
WHEN MATCHED THEN UPDATE SET T.Name_JA = S.Name_JA, T.Name_EN = S.Name_EN
WHEN NOT MATCHED BY TARGET THEN INSERT (Id, Name_JA, Name_EN) VALUES (Id, Name_JA, Name_EN);