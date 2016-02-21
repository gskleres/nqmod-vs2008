// Defines.h
#pragma once

#ifndef DEFINES_LIST
// List of defines that are supposed to be for new additions and can be easily toggled on and off (plus they help with merging)
#define DEFINES_LIST

/// Eggs: Randomises who gets first turn.
#define NQM_RANDOM_FIRST_TURN
/// Eggs: Makes it so barbarians cannot spawn and move in the same turn.
#define NQM_BARBARIANS_MOVE_BEFORE_SPAWNING
/// Eggs: If more than player finish a world wonder on the same turn, the player with the highest production overflow is the one who gets it, with tie breakers randomly decided.
#define NQM_SAME_TURN_WORLD_WONDERS_DECIDED_BY_PRODUCTION_OVERFLOW
/// Eggs: Sets human cities to production focus by default.
#define NQM_HUMAN_CITIES_PRODUCTION_FOCUS_DEFAULT

#endif
