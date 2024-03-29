﻿MERGE INTO Prefectures T
USING (
	SELECT 01 AS Id, N'北海道' AS SubdivisionName_JA, 'Hokkaido' AS SubdivisionName_EN
	UNION ALL
	SELECT 02 AS Id, N'青森県' AS SubdivisionName_JA, 'Aomori' AS SubdivisionName_EN
	UNION ALL
	SELECT 03 AS Id, N'岩手県' AS SubdivisionName_JA, 'Iwate' AS SubdivisionName_EN
	UNION ALL
	SELECT 04 AS Id, N'宮城県' AS SubdivisionName_JA, 'Miyagi' AS SubdivisionName_EN
	UNION ALL
	SELECT 05 AS Id, N'秋田県' AS SubdivisionName_JA, 'Akita' AS SubdivisionName_EN
	UNION ALL
	SELECT 06 AS Id, N'山形県' AS SubdivisionName_JA, 'Yamagata' AS SubdivisionName_EN
	UNION ALL
	SELECT 07 AS Id, N'福島県' AS SubdivisionName_JA, 'Fukushima' AS SubdivisionName_EN
	UNION ALL
	SELECT 08 AS Id, N'茨城県' AS SubdivisionName_JA, 'Ibaraki' AS SubdivisionName_EN
	UNION ALL
	SELECT 09 AS Id, N'栃木県' AS SubdivisionName_JA, 'Tochigi' AS SubdivisionName_EN
	UNION ALL
	SELECT 10 AS Id, N'群馬県' AS SubdivisionName_JA, 'Gunma' AS SubdivisionName_EN
	UNION ALL
	SELECT 11 AS Id, N'埼玉県' AS SubdivisionName_JA, 'Saitama' AS SubdivisionName_EN
	UNION ALL
	SELECT 12 AS Id, N'千葉県' AS SubdivisionName_JA, 'Chiba' AS SubdivisionName_EN
	UNION ALL
	SELECT 13 AS Id, N'東京都' AS SubdivisionName_JA, 'Tokyo' AS SubdivisionName_EN
	UNION ALL
	SELECT 14 AS Id, N'神奈川県' AS SubdivisionName_JA, 'Kanagawa' AS SubdivisionName_EN
	UNION ALL
	SELECT 15 AS Id, N'新潟県' AS SubdivisionName_JA, 'Niigata' AS SubdivisionName_EN
	UNION ALL
	SELECT 16 AS Id, N'富山県' AS SubdivisionName_JA, 'Toyama' AS SubdivisionName_EN
	UNION ALL
	SELECT 17 AS Id, N'石川県' AS SubdivisionName_JA, 'Ishikawa' AS SubdivisionName_EN
	UNION ALL
	SELECT 18 AS Id, N'福井県' AS SubdivisionName_JA, 'Fukui' AS SubdivisionName_EN
	UNION ALL
	SELECT 19 AS Id, N'山梨県' AS SubdivisionName_JA, 'Yamanashi' AS SubdivisionName_EN
	UNION ALL
	SELECT 20 AS Id, N'長野県' AS SubdivisionName_JA, 'Nagano' AS SubdivisionName_EN
	UNION ALL
	SELECT 21 AS Id, N'岐阜県' AS SubdivisionName_JA, 'Gifu' AS SubdivisionName_EN
	UNION ALL
	SELECT 22 AS Id, N'静岡県' AS SubdivisionName_JA, 'Shizuoka' AS SubdivisionName_EN
	UNION ALL
	SELECT 23 AS Id, N'愛知県' AS SubdivisionName_JA, 'Aichi' AS SubdivisionName_EN
	UNION ALL
	SELECT 24 AS Id, N'三重県' AS SubdivisionName_JA, 'Mie' AS SubdivisionName_EN
	UNION ALL
	SELECT 25 AS Id, N'滋賀県' AS SubdivisionName_JA, 'Shiga' AS SubdivisionName_EN
	UNION ALL
	SELECT 26 AS Id, N'京都府' AS SubdivisionName_JA, 'Kyoto' AS SubdivisionName_EN
	UNION ALL
	SELECT 27 AS Id, N'大阪府' AS SubdivisionName_JA, 'Osaka' AS SubdivisionName_EN
	UNION ALL
	SELECT 28 AS Id, N'兵庫県' AS SubdivisionName_JA, 'Hyogo' AS SubdivisionName_EN
	UNION ALL
	SELECT 29 AS Id, N'奈良県' AS SubdivisionName_JA, 'Nara' AS SubdivisionName_EN
	UNION ALL
	SELECT 30 AS Id, N'和歌山県' AS SubdivisionName_JA, 'Wakayama' AS SubdivisionName_EN
	UNION ALL
	SELECT 31 AS Id, N'鳥取県' AS SubdivisionName_JA, 'Tottori' AS SubdivisionName_EN
	UNION ALL
	SELECT 32 AS Id, N'島根県' AS SubdivisionName_JA, 'Shimane' AS SubdivisionName_EN
	UNION ALL
	SELECT 33 AS Id, N'岡山県' AS SubdivisionName_JA, 'Okayama' AS SubdivisionName_EN
	UNION ALL
	SELECT 34 AS Id, N'広島県' AS SubdivisionName_JA, 'Hiroshima' AS SubdivisionName_EN
	UNION ALL
	SELECT 35 AS Id, N'山口県' AS SubdivisionName_JA, 'Yamaguchi' AS SubdivisionName_EN
	UNION ALL
	SELECT 36 AS Id, N'徳島県' AS SubdivisionName_JA, 'Tokushima' AS SubdivisionName_EN
	UNION ALL
	SELECT 37 AS Id, N'香川県' AS SubdivisionName_JA, 'Kagawa' AS SubdivisionName_EN
	UNION ALL
	SELECT 38 AS Id, N'愛媛県' AS SubdivisionName_JA, 'Ehime' AS SubdivisionName_EN
	UNION ALL
	SELECT 39 AS Id, N'高知県' AS SubdivisionName_JA, 'Kochi' AS SubdivisionName_EN
	UNION ALL
	SELECT 40 AS Id, N'福岡県' AS SubdivisionName_JA, 'Fukuoka' AS SubdivisionName_EN
	UNION ALL
	SELECT 41 AS Id, N'佐賀県' AS SubdivisionName_JA, 'Saga' AS SubdivisionName_EN
	UNION ALL
	SELECT 42 AS Id, N'長崎県' AS SubdivisionName_JA, 'Nagasaki' AS SubdivisionName_EN
	UNION ALL
	SELECT 43 AS Id, N'熊本県' AS SubdivisionName_JA, 'Kumamoto' AS SubdivisionName_EN
	UNION ALL
	SELECT 44 AS Id, N'大分県' AS SubdivisionName_JA, 'Oita' AS SubdivisionName_EN
	UNION ALL
	SELECT 45 AS Id, N'宮崎県' AS SubdivisionName_JA, 'Miyazaki' AS SubdivisionName_EN
	UNION ALL
	SELECT 46 AS Id, N'鹿児島県' AS SubdivisionName_JA, 'Kagoshima' AS SubdivisionName_EN
	UNION ALL
	SELECT 47 AS Id, N'沖縄県' AS SubdivisionName_JA, 'Okinawa' AS SubdivisionName_EN
) AS S ON T.Id = S.Id
WHEN MATCHED THEN UPDATE SET T.Id = S.Id, T.SubdivisionName_JA = S.SubdivisionName_JA, T.SubdivisionName_EN = S.SubdivisionName_EN
WHEN NOT MATCHED BY TARGET THEN INSERT VALUES (Id, SubdivisionName_JA, SubdivisionName_EN);