function OnBuildingCheckIfMalianTreasury(iPlayer, iCity, eBuildingType)
	local pPlayer = Players[iPlayer];
	local pCity = pPlayer:GetCityByID(iCity);
	if (eBuildingType == GameInfoTypes.BUILDING_MALIAN_TREASURY) then

		-- get valid plots
		local validPlots = {};
		local iValidPlotsIndex = 0;
		for i = 1, pCity:GetNumCityPlots() - 1, 1 do
			local pPlot = pCity:GetCityIndexPlot( i );
			local iTerrain = pPlot:GetTerrainType();
			if (pPlot ~= nil) then
				if (pPlot:GetOwner() == pCity:GetOwner() and not pPlot:IsWater()) then
					if (pPlot:GetImprovementType() == -1 and pPlot:GetResourceType(-1) == -1) then
						if (iTerrain == TerrainTypes.TERRAIN_DESERT or iTerrain == TerrainTypes.TERRAIN_PLAINS or iTerrain == TerrainTypes.TERRAIN_TUNDRA or iTerrain == TerrainTypes.TERRAIN_GRASS) then
							if (pPlot:GetFeatureType() == -1 or pPlot:GetFeatureType() == FeatureTypes.FEATURE_FOREST or pPlot:GetFeatureType() == FeatureTypes.FEATURE_JUNGLE) then
								iValidPlotsIndex = iValidPlotsIndex + 1;
								validPlots[iValidPlotsIndex] = i;
							end
						end
					end
				end
			end
		end

		-- if we have any valid plots, pick a resource based on the type of terrain we picked
		if (iValidPlotsIndex > 0) then
			local iIndex = math.random(iValidPlotsIndex);
			local pPlot = pCity:GetCityIndexPlot(validPlots[iIndex]);
			local iTerrain = pPlot:GetTerrainType();
			if (pPlot ~= nil) then
				local iGoldWeight = 0;
				local iSilverWeight = 0;
				local iCopperWeight = 0;
				local iSaltWeight = 0;

				if (iTerrain == TerrainTypes.TERRAIN_DESERT) then
					iGoldWeight = 2;
					iSilverWeight = 1;
					iCopperWeight = 1;
					iSaltWeight = 2;
				elseif (iTerrain == TerrainTypes.TERRAIN_PLAINS) then
					iGoldWeight = 1;
					iSilverWeight = 1;
					iCopperWeight = 1;
					iSaltWeight = 2;
				elseif (iTerrain == TerrainTypes.TERRAIN_TUNDRA) then
					iGoldWeight = 1;
					iSilverWeight = 2;
					iCopperWeight = 2;
					iSaltWeight = 1;
				elseif (iTerrain == TerrainTypes.TERRAIN_GRASS) then
					iGoldWeight = 1;
					iSilverWeight = 1;
					iCopperWeight = 1;
					iSaltWeight = 0;
				end

				if (pPlot:IsHills()) then
					iSaltWeight = 0; -- no salt on hills
				end
				
				local iTotalWeight = iGoldWeight + iSilverWeight + iCopperWeight + iSaltWeight;
				
				if (iTotalWeight > 0) then
					local iGoldThreshold = iGoldWeight;
					local iSilverThreshold = iGoldWeight + iSilverWeight;
					local iCopperThreshold = iGoldWeight + iSilverWeight + iCopperWeight;

					local iResourceType = math.random(iTotalWeight);
					if (iResourceType <= iGoldThreshold) then
						pPlot:SetResourceType(GameInfoTypes.RESOURCE_GOLD, 1);
					elseif (iResourceType <= iSilverThreshold) then
						pPlot:SetResourceType(GameInfoTypes.RESOURCE_SILVER, 1);
					elseif (iResourceType <= iCopperThreshold) then
						pPlot:SetResourceType(GameInfoTypes.RESOURCE_COPPER, 1);
					else
						pPlot:SetResourceType(GameInfoTypes.RESOURCE_SALT, 1);
					end
				end
			end
		end
	end
end
GameEvents.CityConstructed.Add(OnBuildingCheckIfMalianTreasury)