-----------------------------------------
-- *** IDEOLOGY UNIVERSAL CHANGES *** ---
-----------------------------------------
-- Number of extra spies granted from the policy
ALTER TABLE Policies ADD NumExtraSpies INTEGER DEFAULT 0;
-- (really ugly code) Gives a free Great Artist whenever you get a Great General if you have this policy
ALTER TABLE Policies ADD IsWarHero BOOLEAN DEFAULT false;

--------------------------------
-- *** AUTOCRACY CHANGES *** ---
--------------------------------
-- Influence gained when tributing gold from a city state (instead of losing it)
ALTER TABLE Policies ADD MinorFriendshipGainBullyGoldSuccess INTEGER DEFAULT 0;
-- Flat change to % chance for all city state coups
ALTER TABLE Policies ADD CoupChanceModifier INTEGER DEFAULT 0;

------------------------------
-- *** FREEDOM CHANGES *** ---
------------------------------
-- Influence modifier (percentage) when conducting trade missions (great merchant)
ALTER TABLE Policies ADD TradeMissionInfluenceModifier INTEGER DEFAULT 0;
-- Science gained whenever a great person is born
ALTER TABLE Policies ADD SciencePerGreatPersonBorn INTEGER DEFAULT 0;
-- Percentage of trade mission (great merchant) gold converted to immediate flat tourism (like futurism, but on trade mission)
ALTER TABLE Policies ADD TourismFromTradeMissions INTEGER DEFAULT 0;
-- Percentage of empire's positive gold per turn income that is converted into science per turn
ALTER TABLE Policies ADD GoldToScience INTEGER DEFAULT 0;
-- Happiness gained per great person tile improved in borders
ALTER TABLE Policies ADD HappinessFromGreatImprovements INTEGER DEFAULT 0;

----------------------------
-- *** ORDER CHANGES *** ---
----------------------------
-- percentage reduction in the increased science cost per city (5%) ... ex: "-20" here = -20% which is (5% * 0.8 = 4%)
ALTER TABLE Policies ADD NumCitiesResearchCostDiscount INTEGER DEFAULT 0;
-- flat tourism boost in each city
ALTER TABLE Policies ADD TourismPerCity INTEGER DEFAULT 0;
-- flat production boost from internal trade routes (both food and production routes)
ALTER TABLE Policies ADD InternalTradeRouteProductionYieldChange INTEGER DEFAULT 0;
-- toggle that disables resistance time in conquered cities if this policy is active
ALTER TABLE Policies ADD DisablesResistanceTime BOOLEAN DEFAULT false;
-- percentage modifier to unhappiness applied via ideological pressure from other civs (ex: -75 = only 25% of normal unhappiness)
ALTER TABLE Policies ADD IdeologyPressureUnhappinessModifier INTEGER DEFAULT 0;
-- cheat implementation - if true, free Research Lab (at plastics) and free Spaceship Factory (at robotics) in every city
ALTER TABLE Policies ADD IsSpaceflightPioneers BOOLEAN DEFAULT false;
-- combat bonus when fighting civs with fewer cities (basically opposite of Ethiopia UA)
ALTER TABLE Policies ADD CombatBonusVsSmallerCiv INTEGER DEFAULT 0;