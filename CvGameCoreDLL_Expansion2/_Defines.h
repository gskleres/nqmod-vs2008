// Defines.h
#pragma once

#ifndef DEFINES_LIST
// List of defines that are supposed to be for new additions and can be easily toggled on and off (plus they help with merging)
#define DEFINES_LIST


/// Turn timers are paused when a player is reconnecting
#define AUI_GAME_SET_PAUSED_TURN_TIMERS_PAUSE_ON_RECONNECT
/// Disables the check for whether a unit is currently embarked for triggering Denmark's UA, so the pathfinder can use it properly
#define AUI_UNIT_MOVEMENT_FIX_BAD_VIKING_DISEMBARK_PREVIEW
/// Fixes Iroquois' UA so friendly forest tiles will now connect with road tiles!
#define AUI_UNIT_MOVEMENT_IROQUOIS_ROAD_TRANSITION_FIX
/// Fixes the fact that in simultaneous turns multiplayer, barbarians can spawn and then move units in the same turn.
#define AUI_GAME_FIX_MULTIPLAYER_BARBARIANS_SPAWN_AFTER_MOVING


// Deliberate AI hindrances
/// AI players will no longer spread their religion to other human players' cities
#define NQM_AI_GIMP_NO_RELIGION_SPREAD
/// AI players will no longer attempt to build any world wonders or world projects
#define NQM_AI_GIMP_NO_WORLD_WONDERS


// AI fixes that significantly affect MP
/// If a player does not have any non-scouting military units, the "enough workers" city strategy is triggered and the "want workers" and "need workers" city strategies always return false
#define AUI_CITYSTRATEGY_DONT_EMPHASIZE_WORKERS_IF_NO_MILITARY


// GlobalDefines (GD) wrappers
// INT
#define GD_INT_DECL(name)       int m_i##name
#define GD_INT_DEF(name)        inline int get##name() { return m_i##name; }
#define GD_INT_CACHE(name)      m_i##name = getDefineINT(#name)


#endif
