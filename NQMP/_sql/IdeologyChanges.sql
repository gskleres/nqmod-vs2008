--------------------------------
-- *** UNIVERSAL CHANGES *** ---
--------------------------------
-- Number of extra spies granted from the policy
ALTER TABLE Policies ADD NumExtraSpies INTEGER DEFAULT 0;

--------------------------------
-- *** AUTOCRACY CHANGES *** ---
--------------------------------
-- Influence gained when tributing gold from a city state (instead of losing it)
ALTER TABLE Policies ADD MinorFriendshipGainBullyGoldSuccess INTEGER DEFAULT 0;