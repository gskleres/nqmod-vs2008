-- AmericaChanges
-- Author: Fruitstrike-L
-- DateCreated: 2016-09-03 08:39:31
--------------------------------------------------------------
-- American Pioneers become workers if they found a non-Capital city
function NQ_American_Pioneer_OnFoundCity(iPlayer, iCityX, iCityY)
	local player = Players[iPlayer]
	if player:GetCivilizationType() == GameInfoTypes.CIVILIZATION_AMERICA then
		local pCapital = player:GetCapitalCity()
		if (pCapital:GetX() ~= iCityX or pCapital:GetY() ~= iCityY) then
			player:InitUnit(GameInfoTypes["UNIT_WORKER"], iCityX, iCityY)
		end
	end
end
GameEvents.PlayerCityFounded.Add(NQ_American_Pioneer_OnFoundCity)