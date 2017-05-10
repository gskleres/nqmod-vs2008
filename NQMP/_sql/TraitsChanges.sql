-- city state allies
ALTER TABLE Traits ADD NumTurnsBeforeMinorAlliesRefuseBribes INTEGER DEFAULT 0;
ALTER TABLE Traits ADD SeaTradeRoutesArePlunderImmune BOOLEAN DEFAULT false;