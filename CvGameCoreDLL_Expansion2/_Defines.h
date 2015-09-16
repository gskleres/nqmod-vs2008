// Defines.h
#pragma once

#ifndef DEFINES_LIST
// List of defines that are supposed to be for new additions and can be easily toggled on and off (plus they help with merging)
#define DEFINES_LIST

// New mathematical constants
// Delnar: these could be calculated at compile-time instead of needing defines, but I don't trust the VC compiler
#define M_E			2.71828182845904523536
#define fM_E		2.718281828f		//!< e (float)
#define M_SQRT2		1.41421356237309504880
#define fM_SQRT2	1.414213562f		//!< sqrt(2) (float)
#define M_SQRT3		1.73205080756887729353
#define fM_SQRT3	1.732050808f		//!< sqrt(3) (float)
#define M_LN2		0.693147180559945309417
#define fM_LN2		0.6931471806f		//!< ln(2) (float)
#define M_GLDNRT	1.61803398874989484820
#define fM_GLDNRT	1.618033989f		//!< (1 + sqrt(5))/2 (float), aka The Golden Ratio

// Technical Improvements
/// New GUID for NQMod
#define NQM_GUID
/// Enables Minidump Generation (originally for Civ4 by terkhen, ported to Civ5 by ls612)
#define NQM_MINIDUMPS
/// Can cache doubles from XML (Delnar: DatabaseUtility actually supports double-type, don't know why Firaxis didn't bother putting this in for good measure)
#define NQM_CACHE_DOUBLE
/// Removes unused functions that simply increase filesize of the DLL without providing any benefit
#define NQM_PRUNING
/// Fast comparison functions (to be used for built-in types like int, float, double, etc.)
#define NQM_FAST_COMP
/// Performance optimizations related to bit twiddling (http://www.graphics.stanford.edu/~seander/bithacks.html)
#define NQM_GAME_CORE_UTILS_OPTIMIZATIONS
/// CvWeightedVector's Top n Choices function now uses unsigned integers for indexes and choice numbers
#define AUI_WEIGHTED_VECTOR_FIX_TOP_CHOICES_USE_UNSIGNED
/// Optimizations and fixes to reduce distance check overhead
#define AUI_FIX_HEX_DISTANCE_INSTEAD_OF_PLOT_DISTANCE
/// Tweaks to make performance logs a bit more consistent and easier to read
#define AUI_PERF_LOGGING_FORMATTING_TWEAKS
/// Slightly increases stopwatch (performance counter) precision by performing the time delta subtraction bit before casting the result as a double
#define AUI_STOPWATCH_SUBTRACT_BEFORE_DELTA_CAST
/// Implements the missing iterator typedefs and empty push_back() functions for BaseVector (so it can now work as an interface... because C++ still makes using interfaces harder than it should be)
#define AUI_FIX_FFASTVECTOR_BASEVECTOR_INTERFACE
/// Implements the missing erase(iterator) function for FFastVector
#define AUI_FIX_FFASTVECTOR_ERASE
/// Functions that called ints for variables used for indexes and choice numbers now call unsigned ints instead
#define AUI_FIX_FFASTVECTOR_USE_UNSIGNED
/// Minor optimizations to some FFastVector classes, justified by the fact that these classes are used extremely often and speed is of the essence when accessing their methods
#define AUI_FIX_FFASTVECTOR_OPTIMIZATIONS
/// Optimized parts of functions responsible for updating plot vision code
#define AUI_PLOT_VISIBILITY_OPTIMIZATIONS

// Fixes to game bugs
/// Removes the cap of 8 range for unit sight; this was only needed because the for() loops weren't set up properly, resulting in too many unused cycles
#define AUI_PLOT_SEE_FROM_SIGHT_NO_MAXIMUM_SIGHT_RANGE
/// When choosing the top n choices from a weighted vector, choices with weight equal to the last choice are also included
#define AUI_WEIGHTED_VECTOR_FIX_TOP_CHOICES_TIE
/// In Hotseat (and now for all multiplayer modes), wars are now declared at the beginning of the AI's turn rather than at the beginning of the human player's turn
#define AUI_DIPLOMACY_AI_FIX_WAR_DECLARATION_IN_MULTIPLAYER
/// Turn timers are paused when a player is reconnecting
#define AUI_GAME_SET_PAUSED_TURN_TIMERS_PAUSE_ON_RECONNECT
/// If the player receives a yield from a goody hut, floating text appears above the plot indicating the number and type of yields received
#define AUI_PLAYER_RECEIVE_GOODY_PLOT_MESSAGE_FOR_YIELD
/// Disables the check for whether a unit is currently embarked for triggering Denmark's UA, so the pathfinder can use it properly
#define AUI_UNIT_MOVEMENT_FIX_BAD_VIKING_DISEMBARK_PREVIEW
/// The allows water walk check is fixed to no longer trigger if water walk improvements are not built adjacent to each other
#define AUI_UNIT_MOVEMENT_FIX_BAD_ALLOWS_WATER_WALK_CHECK
/// Fixes a possible null pointer dereferences in FoundPantheon()
#define AUI_RELIGION_FIX_FOUND_PANTHEON_NULL_POINTER_DEREFERENCE
/// Changes the code that activates "We are Family" to work properly. Since achievements are disabled for modded games, this only works if the mod is turned into mock DLC
#define AUI_ACHIEVEMENT_FIX_RELIGION_WE_ARE_FAMILY_WORKING
/// Promotions that grant air combat bonuses are now allowed for units with no ability for air combat if the promotion also grants the ability for air combat
#define AUI_UNIT_FIX_ALLOW_COMBO_AIR_COMBAT_PROMOTIONS
/// Fixes rarer cases of radar
#define AUI_UNIT_FIX_RADAR
/// Fixes cases of indirect radaring via ZOC.
#define AUI_UNIT_MOVEMENT_FIX_RADAR_ZOC

// Observer mode fixes
/// Observers will see all resources
#define AUI_PLOT_OBSERVER_SEE_ALL_RESOURCES
/// Observers will have constant visibility over all plots
#define AUI_PLOT_OBSERVER_SEE_ALL_PLOTS
/// Observers are set to have met every team in the game
#define AUI_GAME_OBSERVER_MEET_ALL_TEAMS
/// Natural wonder popups do not trigger for observers
#define AUI_PLOT_OBSERVER_NO_NW_POPUPS
/// Observers can now open the city screen
#define AUI_GAME_OBSERVER_CAN_OPEN_CITIES
/// All cities are set to be revealed to observers
#define AUI_CITY_OBSERVER_REVEALS_ALL_CITIES

// Binomial RNG Stuff (Delnar: the binomial RNG generates numbers in a binomial distribution instead of a flat one like the regular RNG)
/// Enables the Binomial Random Number Generator (originally from Artificial Unintelligence)
#define AUI_BINOM_RNG
#ifdef AUI_BINOM_RNG
/// Generated ridges use the binomial RNG for directional bias strength and ridge weakness
#define AUI_FRACTAL_RIDGE_USE_BINOM_RNG
/// When modifying a personality value (eg. Boldness, Wonder Competitiveness), the AI will use the binomial RNG for a normal distribution instead of a flat one
#define AUI_DIPLOMACY_GET_RANDOM_PERSONALITY_WEIGHT_USES_BINOM_RNG
/// When adding or subtracting flavor value, the binomial RNG is used to generate a normal distribution instead of a flat one
#define AUI_FLAVOR_MANAGER_GET_ADJUSTED_VALUE_USES_BINOM_RNG
#endif

// Flavor Manager Stuff
/// Fixes the "zero'ed out flavor" check to still accept 0 as a possible flavor value, but not accept negative values
#define AUI_FLAVOR_MANAGER_FIX_RANDOMIZE_WEIGHTS_ZEROED_OUT_FLAVOR
/// Fixes the function messing up and returning the wrong adjustment when the value to be added is actually negative (eg. for minor civs)
#define AUI_FLAVOR_MANAGER_FIX_GET_ADJUSTED_VALUE_NEGATIVE_PLUSMINUS
/// If the first adjusted value is out of bounds, keep rerolling with the amount with which it is out of bounds until we remain in bounds
#define AUI_FLAVOR_MANAGER_GET_ADJUSTED_VALUE_USE_REROLLS

// GlobalDefines (GD) wrappers
// INT
#define GD_INT_DECL(name)       int m_i##name
#define GD_INT_DEF(name)        inline int get##name() { return m_i##name; }
#define GD_INT_INIT(name, def)  m_i##name(def)
#define GD_INT_CACHE(name)      m_i##name = getDefineINT(#name)
#define GD_INT_GET(name)        GC.get##name()
// FLOAT
#define GD_FLOAT_DECL(name)       float m_f##name
#define GD_FLOAT_DEF(name)        inline float get##name() { return m_f##name; }
#define GD_FLOAT_INIT(name, def)  m_f##name(def)
#define GD_FLOAT_CACHE(name)      m_f##name = getDefineFLOAT(#name)
#define GD_FLOAT_GET(name)        GC.get##name()
// DOUBLE (high precision, but much slower than float)
#ifdef NQM_CACHE_DOUBLE
#define GD_DOUBLE_DECL(name)       double m_d##name
#define GD_DOUBLE_DEF(name)        inline double get##name() { return m_d##name; }
#define GD_DOUBLE_INIT(name, def)  m_d##name(def)
#define GD_DOUBLE_CACHE(name)      m_d##name = getDefineDOUBLE(#name)
#define GD_DOUBLE_GET(name)        GC.get##name()
#endif

#endif