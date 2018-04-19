-- *****************************************************************************
-- This script contains INSERT statements for populating tables with seed data
-- *****************************************************************************



-- INSERT statements go here


USE [HotelFlashcards]
GO
SET IDENTITY_INSERT [dbo].[users] ON 

INSERT [dbo].[users] ([UserID], [Email], [Password], [IsAdmin], [DisplayName]) VALUES (1, 'admin@hotel.com', 'password', 1, 'hotel_admin')
INSERT [dbo].[users] ([UserID], [Email], [Password], [IsAdmin], [DisplayName]) VALUES (2, 'user@hotel.com', 'password', 0, 'test_user')
INSERT [dbo].[users] ([UserID], [Email], [Password], [IsAdmin], [DisplayName]) VALUES (3, 'user2@hotel.com', 'password', 0, 'second_user')
SET IDENTITY_INSERT [dbo].[users] OFF

SET IDENTITY_INSERT [dbo].[decks] ON 
INSERT [dbo].[decks] ([DeckID], [UserID], [Name], [IsPublic]) VALUES (1, 2, 'State Capitals', 0)
SET IDENTITY_INSERT [dbo].[decks] OFF

SET IDENTITY_INSERT [dbo].[cards] ON 
INSERT [dbo].[cards] ([CardID], [UserID], [Front], [Back]) VALUES (1, 2, 'Alabama', 'Montgomery')
INSERT [dbo].[cards] ([CardID], [UserID], [Front], [Back]) VALUES (2, 2, 'Arkansas', 'Little Rock')
INSERT [dbo].[cards] ([CardID], [UserID], [Front], [Back]) VALUES (3, 2, 'Florida', 'Tallahassee')
INSERT [dbo].[cards] ([CardID], [UserID], [Front], [Back]) VALUES (4, 2, 'Atlanta', 'Georgia')
INSERT [dbo].[cards] ([CardID], [UserID], [Front], [Back]) VALUES (5, 2, 'Jackson', 'Mississippi')
INSERT [dbo].[cards] ([CardID], [UserID], [Front], [Back]) VALUES (6, 2, 'South Carolina', 'Columbia')
INSERT [dbo].[cards] ([CardID], [UserID], [Front], [Back]) VALUES (7, 2, 'North Carolina', 'Raleigh')
INSERT [dbo].[cards] ([CardID], [UserID], [Front], [Back]) VALUES (8, 2, 'Ohio', 'Cincinnati')
INSERT [dbo].[cards] ([CardID], [UserID], [Front], [Back]) VALUES (9, 1, 'Kentucky', 'Frankfurt')
INSERT [dbo].[cards] ([CardID], [UserID], [Front], [Back]) VALUES (10, 1, 'Tennessee', 'Nashville')
SET IDENTITY_INSERT [dbo].[cards] OFF

INSERT [dbo].[card_deck] ([DeckID], [CardID]) VALUES (1, 1)
INSERT [dbo].[card_deck] ([DeckID], [CardID]) VALUES (1, 2)
INSERT [dbo].[card_deck] ([DeckID], [CardID]) VALUES (1, 3)
INSERT [dbo].[card_deck] ([DeckID], [CardID]) VALUES (1, 4)
INSERT [dbo].[card_deck] ([DeckID], [CardID]) VALUES (1, 5)

SET IDENTITY_INSERT [dbo].[tags] ON 
INSERT [dbo].[tags] ([TagID], [TagName]) VALUES (1, 'United States')
INSERT [dbo].[tags] ([TagID], [TagName]) VALUES (2, 'Southern State')
INSERT [dbo].[tags] ([TagID], [TagName]) VALUES (3, 'Capitals')
SET IDENTITY_INSERT [dbo].[tags] OFF

INSERT [dbo].[deck_tag] ([DeckID], [TagID]) VALUES (1, 1)
INSERT [dbo].[deck_tag] ([DeckID], [TagID]) VALUES (1, 3)

INSERT [dbo].[card_tag] ([CardID], [TagID]) VALUES (1, 2)
INSERT [dbo].[card_tag] ([CardID], [TagID]) VALUES (2, 2)

