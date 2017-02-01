USE [Database1111]
GO
SET IDENTITY_INSERT [dbo].[ДомSet] ON 

GO
INSERT [dbo].[ДомSet] ([Id], [Адрес], [Площадь]) VALUES (1, N'ул. Большая Спасская, 10\1', CAST(1240 AS Decimal(18, 0)))
GO
SET IDENTITY_INSERT [dbo].[ДомSet] OFF
GO
SET IDENTITY_INSERT [dbo].[КвартираSet] ON 

GO
INSERT [dbo].[КвартираSet] ([Id], [Площадь_квартиры], [Номер_квартиры], [Дом_Id]) VALUES (1, CAST(48 AS Decimal(18, 0)), 235, 1)
GO
INSERT [dbo].[КвартираSet] ([Id], [Площадь_квартиры], [Номер_квартиры], [Дом_Id]) VALUES (2, CAST(94 AS Decimal(18, 0)), 236, 1)
GO
INSERT [dbo].[КвартираSet] ([Id], [Площадь_квартиры], [Номер_квартиры], [Дом_Id]) VALUES (3, CAST(48 AS Decimal(18, 0)), 237, 1)
GO
INSERT [dbo].[КвартираSet] ([Id], [Площадь_квартиры], [Номер_квартиры], [Дом_Id]) VALUES (4, CAST(120 AS Decimal(18, 0)), 238, 1)
GO
INSERT [dbo].[КвартираSet] ([Id], [Площадь_квартиры], [Номер_квартиры], [Дом_Id]) VALUES (5, CAST(240 AS Decimal(18, 0)), 239, 1)
GO
INSERT [dbo].[КвартираSet] ([Id], [Площадь_квартиры], [Номер_квартиры], [Дом_Id]) VALUES (6, CAST(240 AS Decimal(18, 0)), 240, 1)
GO
INSERT [dbo].[КвартираSet] ([Id], [Площадь_квартиры], [Номер_квартиры], [Дом_Id]) VALUES (7, CAST(240 AS Decimal(18, 0)), 241, 1)
GO
INSERT [dbo].[КвартираSet] ([Id], [Площадь_квартиры], [Номер_квартиры], [Дом_Id]) VALUES (8, CAST(120 AS Decimal(18, 0)), 242, 1)
GO
SET IDENTITY_INSERT [dbo].[КвартираSet] OFF
GO
SET IDENTITY_INSERT [dbo].[КалендарьSet] ON 

GO
INSERT [dbo].[КалендарьSet] ([Id], [Месяц], [Год]) VALUES (1, N'01', N'2016')
GO
INSERT [dbo].[КалендарьSet] ([Id], [Месяц], [Год]) VALUES (2, N'02', N'2016')
GO
INSERT [dbo].[КалендарьSet] ([Id], [Месяц], [Год]) VALUES (3, N'03', N'2016')
GO
INSERT [dbo].[КалендарьSet] ([Id], [Месяц], [Год]) VALUES (4, N'04', N'2016')
GO
INSERT [dbo].[КалендарьSet] ([Id], [Месяц], [Год]) VALUES (5, N'05', N'2016')
GO
INSERT [dbo].[КалендарьSet] ([Id], [Месяц], [Год]) VALUES (6, N'06', N'2016')
GO
INSERT [dbo].[КалендарьSet] ([Id], [Месяц], [Год]) VALUES (7, N'07', N'2016')
GO
INSERT [dbo].[КалендарьSet] ([Id], [Месяц], [Год]) VALUES (8, N'08', N'2016')
GO
INSERT [dbo].[КалендарьSet] ([Id], [Месяц], [Год]) VALUES (9, N'09', N'2016')
GO
INSERT [dbo].[КалендарьSet] ([Id], [Месяц], [Год]) VALUES (10, N'10', N'2016')
GO
INSERT [dbo].[КалендарьSet] ([Id], [Месяц], [Год]) VALUES (11, N'11', N'2016')
GO
INSERT [dbo].[КалендарьSet] ([Id], [Месяц], [Год]) VALUES (12, N'12', N'2016')
GO
INSERT [dbo].[КалендарьSet] ([Id], [Месяц], [Год]) VALUES (13, N'01', N'2017')
GO
INSERT [dbo].[КалендарьSet] ([Id], [Месяц], [Год]) VALUES (14, N'02', N'2017')
GO
INSERT [dbo].[КалендарьSet] ([Id], [Месяц], [Год]) VALUES (16, N'03', N'2017')
GO
INSERT [dbo].[КалендарьSet] ([Id], [Месяц], [Год]) VALUES (17, N'04', N'2017')
GO
INSERT [dbo].[КалендарьSet] ([Id], [Месяц], [Год]) VALUES (18, N'05', N'2017')
GO
INSERT [dbo].[КалендарьSet] ([Id], [Месяц], [Год]) VALUES (19, N'06', N'2017')
GO
INSERT [dbo].[КалендарьSet] ([Id], [Месяц], [Год]) VALUES (20, N'07', N'2017')
GO
INSERT [dbo].[КалендарьSet] ([Id], [Месяц], [Год]) VALUES (21, N'08', N'2017')
GO
INSERT [dbo].[КалендарьSet] ([Id], [Месяц], [Год]) VALUES (22, N'09', N'2017')
GO
INSERT [dbo].[КалендарьSet] ([Id], [Месяц], [Год]) VALUES (23, N'10', N'2017')
GO
INSERT [dbo].[КалендарьSet] ([Id], [Месяц], [Год]) VALUES (24, N'11', N'2017')
GO
INSERT [dbo].[КалендарьSet] ([Id], [Месяц], [Год]) VALUES (25, N'12', N'2017')
GO
SET IDENTITY_INSERT [dbo].[КалендарьSet] OFF
GO
SET IDENTITY_INSERT [dbo].[Показания_ОДУSet] ON 

GO
INSERT [dbo].[Показания_ОДУSet] ([Id], [Показание_ГВС], [Показание_ХВС], [Календарь_Id], [Дом_Id]) VALUES (1, N'2374', N'4657', 13, 1)
GO
INSERT [dbo].[Показания_ОДУSet] ([Id], [Показание_ГВС], [Показание_ХВС], [Календарь_Id], [Дом_Id]) VALUES (2, N'2401', N'4695', 14, 1)
GO
SET IDENTITY_INSERT [dbo].[Показания_ОДУSet] OFF
GO
SET IDENTITY_INSERT [dbo].[УслугаSet] ON 

GO
INSERT [dbo].[УслугаSet] ([Id], [Вид_услуги], [Тариф], [ГВС], [Отопление], [Водоотведение], [Содерж_дома], [ХВС]) VALUES (1, N'ГВС', CAST(10 AS Decimal(18, 0)), 1, 0, 0, 0, N'False')
GO
INSERT [dbo].[УслугаSet] ([Id], [Вид_услуги], [Тариф], [ГВС], [Отопление], [Водоотведение], [Содерж_дома], [ХВС]) VALUES (3, N'ХВС', CAST(2 AS Decimal(18, 0)), 0, 0, 0, 0, N'True')
GO
INSERT [dbo].[УслугаSet] ([Id], [Вид_услуги], [Тариф], [ГВС], [Отопление], [Водоотведение], [Содерж_дома], [ХВС]) VALUES (5, N'Отопление', CAST(25 AS Decimal(18, 0)), 0, 1, 0, 0, N'False')
GO
INSERT [dbo].[УслугаSet] ([Id], [Вид_услуги], [Тариф], [ГВС], [Отопление], [Водоотведение], [Содерж_дома], [ХВС]) VALUES (6, N'Водоотведение', CAST(1 AS Decimal(18, 0)), 0, 0, 1, 0, N'False')
GO
INSERT [dbo].[УслугаSet] ([Id], [Вид_услуги], [Тариф], [ГВС], [Отопление], [Водоотведение], [Содерж_дома], [ХВС]) VALUES (7, N'Сод.общ.дом.', CAST(2 AS Decimal(18, 0)), 0, 0, 0, 1, N'False')
GO
INSERT [dbo].[УслугаSet] ([Id], [Вид_услуги], [Тариф], [ГВС], [Отопление], [Водоотведение], [Содерж_дома], [ХВС]) VALUES (8, N'Антенна', CAST(100 AS Decimal(18, 0)), 0, 0, 0, 0, N'False')
GO
SET IDENTITY_INSERT [dbo].[УслугаSet] OFF
GO
SET IDENTITY_INSERT [dbo].[СчетчикSet] ON 

GO
INSERT [dbo].[СчетчикSet] ([Код_счетчика], [Квартира_Id], [Услуга_Id]) VALUES (1, 2, 1)
GO
INSERT [dbo].[СчетчикSet] ([Код_счетчика], [Квартира_Id], [Услуга_Id]) VALUES (3, 2, 3)
GO
INSERT [dbo].[СчетчикSet] ([Код_счетчика], [Квартира_Id], [Услуга_Id]) VALUES (4, 3, 1)
GO
INSERT [dbo].[СчетчикSet] ([Код_счетчика], [Квартира_Id], [Услуга_Id]) VALUES (5, 3, 3)
GO
INSERT [dbo].[СчетчикSet] ([Код_счетчика], [Квартира_Id], [Услуга_Id]) VALUES (6, 7, 1)
GO
INSERT [dbo].[СчетчикSet] ([Код_счетчика], [Квартира_Id], [Услуга_Id]) VALUES (7, 7, 3)
GO
INSERT [dbo].[СчетчикSet] ([Код_счетчика], [Квартира_Id], [Услуга_Id]) VALUES (8, 8, 1)
GO
INSERT [dbo].[СчетчикSet] ([Код_счетчика], [Квартира_Id], [Услуга_Id]) VALUES (9, 8, 3)
GO
SET IDENTITY_INSERT [dbo].[СчетчикSet] OFF
GO
SET IDENTITY_INSERT [dbo].[ЖилецSet] ON 

GO
INSERT [dbo].[ЖилецSet] ([Id], [ФИО], [Номер_паспорта], [Квартира_Id]) VALUES (4, N'Петров Иван Иванович', N'4510 №182347', 1)
GO
INSERT [dbo].[ЖилецSet] ([Id], [ФИО], [Номер_паспорта], [Квартира_Id]) VALUES (6, N'Сидоров Иван Андреевич', N'4509 №182376', 2)
GO
INSERT [dbo].[ЖилецSet] ([Id], [ФИО], [Номер_паспорта], [Квартира_Id]) VALUES (7, N'Сидорова Марья Ивановна', N'4508 №152345', 2)
GO
INSERT [dbo].[ЖилецSet] ([Id], [ФИО], [Номер_паспорта], [Квартира_Id]) VALUES (10, N'Андреев Иван Петрович', N'4508 №182376', 3)
GO
INSERT [dbo].[ЖилецSet] ([Id], [ФИО], [Номер_паспорта], [Квартира_Id]) VALUES (12, N'Романова Екатерина Ивановна', N'4506 №273645', 4)
GO
INSERT [dbo].[ЖилецSet] ([Id], [ФИО], [Номер_паспорта], [Квартира_Id]) VALUES (13, N'Ежов Павел Иванович', N'4507 №912387', 5)
GO
INSERT [dbo].[ЖилецSet] ([Id], [ФИО], [Номер_паспорта], [Квартира_Id]) VALUES (14, N'Степанова Наталья Ивановна', N'4507 №613450', 6)
GO
INSERT [dbo].[ЖилецSet] ([Id], [ФИО], [Номер_паспорта], [Квартира_Id]) VALUES (15, N'Федорова Ольга Сергеевна', N'4507 №856736', 7)
GO
INSERT [dbo].[ЖилецSet] ([Id], [ФИО], [Номер_паспорта], [Квартира_Id]) VALUES (16, N'Маркина Елена Андреевна', N'4603 №834756', 8)
GO
INSERT [dbo].[ЖилецSet] ([Id], [ФИО], [Номер_паспорта], [Квартира_Id]) VALUES (17, N'Маркин Петр Петрович', N'4604 №968374', 8)
GO
SET IDENTITY_INSERT [dbo].[ЖилецSet] OFF
GO
SET IDENTITY_INSERT [dbo].[Показания_квартирSet] ON 

GO
INSERT [dbo].[Показания_квартирSet] ([Id], [Значение_показания_кв], [Календарь_Id], [Счетчик_Код_счетчика]) VALUES (1, 124, 13, 1)
GO
INSERT [dbo].[Показания_квартирSet] ([Id], [Значение_показания_кв], [Календарь_Id], [Счетчик_Код_счетчика]) VALUES (2, 126, 14, 1)
GO
INSERT [dbo].[Показания_квартирSet] ([Id], [Значение_показания_кв], [Календарь_Id], [Счетчик_Код_счетчика]) VALUES (3, 230, 13, 4)
GO
INSERT [dbo].[Показания_квартирSet] ([Id], [Значение_показания_кв], [Календарь_Id], [Счетчик_Код_счетчика]) VALUES (4, 231, 14, 4)
GO
INSERT [dbo].[Показания_квартирSet] ([Id], [Значение_показания_кв], [Календарь_Id], [Счетчик_Код_счетчика]) VALUES (5, 45, 13, 6)
GO
INSERT [dbo].[Показания_квартирSet] ([Id], [Значение_показания_кв], [Календарь_Id], [Счетчик_Код_счетчика]) VALUES (6, 48, 14, 6)
GO
INSERT [dbo].[Показания_квартирSet] ([Id], [Значение_показания_кв], [Календарь_Id], [Счетчик_Код_счетчика]) VALUES (7, 120, 13, 8)
GO
INSERT [dbo].[Показания_квартирSet] ([Id], [Значение_показания_кв], [Календарь_Id], [Счетчик_Код_счетчика]) VALUES (8, 127, 14, 8)
GO
INSERT [dbo].[Показания_квартирSet] ([Id], [Значение_показания_кв], [Календарь_Id], [Счетчик_Код_счетчика]) VALUES (9, 230, 13, 3)
GO
INSERT [dbo].[Показания_квартирSet] ([Id], [Значение_показания_кв], [Календарь_Id], [Счетчик_Код_счетчика]) VALUES (10, 236, 14, 3)
GO
INSERT [dbo].[Показания_квартирSet] ([Id], [Значение_показания_кв], [Календарь_Id], [Счетчик_Код_счетчика]) VALUES (11, 349, 13, 5)
GO
INSERT [dbo].[Показания_квартирSet] ([Id], [Значение_показания_кв], [Календарь_Id], [Счетчик_Код_счетчика]) VALUES (12, 356, 14, 5)
GO
INSERT [dbo].[Показания_квартирSet] ([Id], [Значение_показания_кв], [Календарь_Id], [Счетчик_Код_счетчика]) VALUES (13, 23, 13, 7)
GO
INSERT [dbo].[Показания_квартирSet] ([Id], [Значение_показания_кв], [Календарь_Id], [Счетчик_Код_счетчика]) VALUES (14, 25, 14, 7)
GO
INSERT [dbo].[Показания_квартирSet] ([Id], [Значение_показания_кв], [Календарь_Id], [Счетчик_Код_счетчика]) VALUES (15, 230, 13, 9)
GO
INSERT [dbo].[Показания_квартирSet] ([Id], [Значение_показания_кв], [Календарь_Id], [Счетчик_Код_счетчика]) VALUES (16, 233, 14, 9)
GO
SET IDENTITY_INSERT [dbo].[Показания_квартирSet] OFF
GO
SET IDENTITY_INSERT [dbo].[ЛьготаSet] ON 

GO
INSERT [dbo].[ЛьготаSet] ([Id], [Имя_льготы], [Значение_льготы], [Общая]) VALUES (1, N'Инвалид 1 группы', CAST(1 AS Decimal(18, 0)), 1)
GO
INSERT [dbo].[ЛьготаSet] ([Id], [Имя_льготы], [Значение_льготы], [Общая]) VALUES (2, N'Инвалид 2 группы', CAST(1 AS Decimal(18, 0)), 0)
GO
SET IDENTITY_INSERT [dbo].[ЛьготаSet] OFF
GO
INSERT [dbo].[ЖилецЛьгота] ([Жилец_Id], [Льгота_Id]) VALUES (10, 1)
GO
INSERT [dbo].[ЖилецЛьгота] ([Жилец_Id], [Льгота_Id]) VALUES (7, 2)
GO
SET IDENTITY_INSERT [dbo].[Соц_нормаSet] ON 

GO
INSERT [dbo].[Соц_нормаSet] ([Id], [Вид_нормы], [Значение_нормы], [Услуга_Id]) VALUES (1, N'ГВС', CAST(3 AS Decimal(18, 0)), 1)
GO
INSERT [dbo].[Соц_нормаSet] ([Id], [Вид_нормы], [Значение_нормы], [Услуга_Id]) VALUES (2, N'ХВС', CAST(6 AS Decimal(18, 0)), 3)
GO
INSERT [dbo].[Соц_нормаSet] ([Id], [Вид_нормы], [Значение_нормы], [Услуга_Id]) VALUES (3, N'Отопление', CAST(25 AS Decimal(18, 0)), 5)
GO
SET IDENTITY_INSERT [dbo].[Соц_нормаSet] OFF
GO
