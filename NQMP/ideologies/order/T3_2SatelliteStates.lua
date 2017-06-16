-- T3_2SatelliteStates
-- Author: Fruitstrike
-- DateCreated: 6/15/2017 2:47:33 PM
--------------------------------------------------------------

-- add dummies as needed when the policy is taken
function T3_2SatelliteStates_OnPolicyAdopted(iPlayerID, iPolicyID)
	local pPlayer = Players[iPlayerID];
	local bHasPolicy = pPlayer:HasPolicy(GameInfo.Policies["POLICY_SKYSCRAPERS"].ID);
	for loopCity in pPlayer:Cities() do
		if (bHasPolicy and loopCity:GetNumBuilding(GameInfo.Buildings["BUILDING_COURTHOUSE"].ID)) then
			loopCity:SetNumRealBuilding(GameInfoTypes["DUMMY_BUILDING_T3_2_SATELLITE_STATES"], 1);
			pPlayer:ChangeGold(10000);
		else
			loopCity:SetNumRealBuilding(GameInfoTypes["DUMMY_BUILDING_T3_2_SATELLITE_STATES"], 0);
		end
	end
end
GameEvents.PlayerAdoptPolicy.Add(T3_2SatelliteStates_OnPolicyAdopted);

-- add dummy when courthouse is constructed
function T3_2SatelliteStates_OnCityConstructed(iPlayerID, iCityID, iBuildingID)
	if (iBuildingID == GameInfoTypes.BUILDING_COURTHOUSE) then
		local pPlayer = Players[iPlayerID];
		if (pPlayer:HasPolicy(GameInfo.Policies["POLICY_SKYSCRAPERS"].ID)) then
			local pCity = pPlayer:GetCityByID(iCityID);
			pCity:SetNumRealBuilding(GameInfoTypes["DUMMY_BUILDING_T3_2_SATELLITE_STATES"], 1);
			pPlayer:ChangeGold(1000);
		end
	end
end
GameEvents.CityConstructed.Add(T3_2SatelliteStates_OnCityConstructed);
