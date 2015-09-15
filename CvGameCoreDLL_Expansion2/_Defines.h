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