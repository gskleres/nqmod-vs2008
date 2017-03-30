function NQMP_T1_Covert_Action_OnPolicyAdopted(playerID, policyID)
	local player = Players[playerID]
	if (policyID == GameInfo.Policies["POLICY_COVERT_ACTION"].ID) then
		local pCapital = player:GetCapitalCity()
		pCapital:SetNumRealBuilding(GameInfoTypes["DUMMY_BUILDING_T1_COVERT_ACTION"], 1)
		pCapital:SetNumRealBuilding(GameInfoTypes["DUMMY_BUILDING_T1_COVERT_ACTION"], 0)
	end
end
GameEvents.PlayerAdoptPolicy.Add(NQMP_T1_Covert_Action_OnPolicyAdopted)