-----------------------------------------
-- *** IDEOLOGY UNIVERSAL CHANGES *** ---
-----------------------------------------
-- Number of extra spies granted from the policy
ALTER TABLE Policies ADD NumExtraSpies INTEGER DEFAULT 0;

--------------------------------
-- *** AUTOCRACY CHANGES *** ---
--------------------------------
-- Influence gained when tributing gold from a city state (instead of losing it)
ALTER TABLE Policies ADD MinorFriendshipGainBullyGoldSuccess INTEGER DEFAULT 0;

------------------------------
-- *** FREEDOM CHANGES *** ---
------------------------------
-- Influence modifier when conducting trade missions (great merchant)
ALTER TABLE Policies ADD TradeMissionInfluenceModifier INTEGER DEFAULT 0;
-- Science gain whenever a great person is born
ALTER TABLE Policies ADD SciencePerGreatPersonBorn INTEGER DEFAULT 0;
-- Percentage of trade mission (great merchant) gold converted to immediate flat tourism (like futurism, but on trade mission)
ALTER TABLE Policies ADD TourismFromTradeMissions INTEGER DEFAULT 0;
-- Percentage of empire's positive gold per turn income that is converted into science per turn
ALTER TABLE Policies ADD GoldToScience INTEGER DEFAULT 0;
-- Happiness gained per great person tile improved in borders
ALTER TABLE Policies ADD HappinessFromGreatImprovements INTEGER DEFAULT 0;