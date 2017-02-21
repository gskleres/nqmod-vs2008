-- Insert SQL Rules Here
ALTER TABLE Policies ADD GuildTourismAndGPPPerMonument INTEGER DEFAULT 0;
ALTER TABLE Buildings ADD IsGuild BOOLEAN DEFAULT false;