-- *****************************************************************************
-- This script contains INSERT statements for populating tables with seed data
-- *****************************************************************************

BEGIN;

-- INSERT statements go here


USE [HotelFlashcards]
GO
SET IDENTITY_INSERT [dbo].[users] ON 

INSERT [dbo].[users] ([UserID], [Email], [Password], [IsAdmin], [UserName]) VALUES (1, 'admin@hotel.com', 'password', 1, 'hotel_admin')
INSERT [dbo].[users] ([UserID], [Email], [Password], [IsAdmin], [UserName]) VALUES (2, 'user@hotel.com', 'password', 0, 'test_user')
SET IDENTITY_INSERT [dbo].[users] OFF

SET IDENTITY_INSERT [dbo].[decks] ON 
INSERT [dbo].[decks] ([DeckID], [UserID], [Name], [IsPublic]) VALUES (1, 2, 'Test Deck', 0)
SET IDENTITY_INSERT [dbo].[decks] OFF

SET IDENTITY_INSERT [dbo].[cards] ON 
INSERT [dbo].[cards] ([CardID], [UserID], [Front], [Back]) VALUES (1, 2, 'What tag is used for paragraphs?', '<p>')
INSERT [dbo].[cards] ([CardID], [UserID], [Front], [Back]) VALUES (2, 2, 'What tag is used for images?', '<img>')
INSERT [dbo].[cards] ([CardID], [UserID], [Front], [Back]) VALUES (3, 2, 'What css property is used to line up containers next to each other?', 'inline-block')
INSERT [dbo].[cards] ([CardID], [UserID], [Front], [Back]) VALUES (4, 2, N'What javascript keyword is used to define variables?', N'var')
SET IDENTITY_INSERT [dbo].[cards] OFF

INSERT [dbo].[card_deck] ([CardID], [DeckID]) VALUES (2, 1)
INSERT [dbo].[card_deck] ([CardID], [DeckID]) VALUES (4, 1)

SET IDENTITY_INSERT [dbo].[tags] ON 
INSERT [dbo].[tags] ([TagID], [TagName]) VALUES (1, N'html')
INSERT [dbo].[tags] ([TagID], [TagName]) VALUES (2, N'css')
INSERT [dbo].[tags] ([TagID], [TagName]) VALUES (3, N'javascript')
INSERT [dbo].[tags] ([TagID], [TagName]) VALUES (4, N'Web Design')
SET IDENTITY_INSERT [dbo].[tags] OFF

INSERT [dbo].[deck_tag] ([TagID], [DeckID]) VALUES (4, 1)
INSERT [dbo].[card_tag] ([TagID], [CardID]) VALUES (1, 1)
INSERT [dbo].[card_tag] ([TagID], [CardID]) VALUES (3, 2)

COMMIT;