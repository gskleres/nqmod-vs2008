function UpdateStreamerView()
	local pActivePlayer = Players[ Game.GetActivePlayer() ];

	-- Policies
	local strPoliciesTextFull = "";
	local strPoliciesTextAbbreviated = "";
	for pPolicyBranch in GameInfo.PolicyBranchTypes() do
		local iPolicyBranch = pPolicyBranch.ID;
					
		local iCount = 0;
		local bBranchOpened = pActivePlayer:IsPolicyBranchUnlocked(iPolicyBranch);

		for pPolicy in GameInfo.Policies() do
			local iPolicy = pPolicy.ID;
						
			if (pPolicy.PolicyBranchType == pPolicyBranch.Type) then
				if (pActivePlayer:HasPolicy(iPolicy)) then
					iCount = iCount + 1;
				end
			end
		end

		if (iCount > 0 or bBranchOpened) then
			if (strPoliciesTextFull ~= "") then
				strPoliciesTextFull = strPoliciesTextFull .. ", ";
				strPoliciesTextAbbreviated = strPoliciesTextAbbreviated .. ", ";
			end

			local strBranchName = Locale.ConvertTextKey(pPolicyBranch.Description);
			--local strBranchNameAbbrev = string.upper(string.sub(strBranchName, 1, 2));
			local strBranchNameAbbrev = string.sub(strBranchName, 1, 3);

			strPoliciesTextFull = strPoliciesTextFull .. strBranchName .. " " .. iCount;
			strPoliciesTextAbbreviated = strPoliciesTextAbbreviated .. strBranchNameAbbrev .. " " .. iCount;
		end
	end

	-- if it's empty, make it look right and grey it out
	if (strPoliciesTextFull == "") then
		strPoliciesText = "[COLOR_GREY]" .. Locale.ConvertTextKey("TXT_KEY_VP_POLICIES") .. ": " .. Locale.ConvertTextKey("TXT_KEY_RO_BELIEFS_NONE") .. "[ENDCOLOR]";
	-- otherwise, if it's too long, use the abbrivated version
	elseif (string.len(strPoliciesTextFull) > 75) then
		strPoliciesText = strPoliciesTextAbbreviated;
	-- otherwise, use the full version
	else
		strPoliciesText = strPoliciesTextFull;
	end

	-- religion strings
	local strReligion1Text = "";
	local strReligion2Text = "";
	if (pActivePlayer:HasCreatedReligion()) then
		local beliefCount = 0;
		local eReligion = pActivePlayer:GetReligionCreatedByPlayer();
		for i,v in ipairs(Game.GetBeliefsInReligion(eReligion)) do
			local belief = GameInfo.Beliefs[v];
			if (belief ~= nil) then
				if (beliefCount < 3) then
					-- first religion line
					if (strReligion1Text ~= "") then
						strReligion1Text = strReligion1Text .. ", ";
					end
					strReligion1Text = strReligion1Text .. Locale.ConvertTextKey(belief.ShortDescription);
				else
					-- second religion line
					if (strReligion2Text == "") then
						strReligion1Text = strReligion1Text .. ", ";
					else
						strReligion2Text = strReligion2Text .. ", ";
					end
					strReligion2Text = strReligion2Text .. Locale.ConvertTextKey(belief.ShortDescription);
				end
				beliefCount = beliefCount + 1;
			end
		end
	elseif (pActivePlayer:HasCreatedPantheon()) then
		local belief = GameInfo.Beliefs[pActivePlayer:GetBeliefInPantheon()];
		if (belief ~= nil) then
			strReligion1Text = Locale.ConvertTextKey("TXT_KEY_RELIGION_PANTHEON") .. ": " .. Locale.ConvertTextKey(belief.ShortDescription);
			strReligion2Text = "[COLOR_GREY]" .. Locale.ConvertTextKey("TXT_KEY_RO_WR_RELIGION") .. ": " .. Locale.ConvertTextKey("TXT_KEY_RO_BELIEFS_NONE") .. "[ENDCOLOR]";
		end
	else
		strReligion1Text = "[COLOR_GREY]" .. Locale.ConvertTextKey("TXT_KEY_RELIGION_PANTHEON") .. ": " .. Locale.ConvertTextKey("TXT_KEY_RO_BELIEFS_NONE") .. "[ENDCOLOR]";
		strReligion2Text = "";
	end

	--Controls.StreamerPoliciesText:SetText("[COLOR:255:0:255:255]" .. strPoliciesText .. "[/COLOR]");
	Controls.StreamerPoliciesText:SetText(strPoliciesText);
	Controls.StreamerBeliefs1Text:SetText(strReligion1Text);
	Controls.StreamerBeliefs2Text:SetText(strReligion2Text);
end

function OnStreamerViewShow()	
    UpdateStreamerView();
	Controls.StreamerPanel:SetHide(false);
	Controls.StreamerViewButtonOpen:SetHide(true);
	Controls.StreamerViewButtonClose:SetHide(false);
end
Controls.StreamerViewButtonOpen:RegisterCallback( Mouse.eLClick, OnStreamerViewShow );

function OnStreamerViewHide()	
	Controls.StreamerPanel:SetHide(true);
	Controls.StreamerViewButtonOpen:SetHide(false);
	Controls.StreamerViewButtonClose:SetHide(true);
end
Controls.StreamerViewButtonClose:RegisterCallback( Mouse.eLClick, OnStreamerViewHide );

GameEvents.PlayerAdoptPolicyBranch.Add(UpdateStreamerView);
GameEvents.PlayerAdoptPolicy.Add(UpdateStreamerView);
GameEvents.PantheonFounded.Add(UpdateStreamerView);
GameEvents.ReligionFounded.Add(UpdateStreamerView);
GameEvents.ReligionEnhanced.Add(UpdateStreamerView);
GameEvents.ReformationAdded.Add(UpdateStreamerView);
UpdateStreamerView();