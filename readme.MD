# AIXM Query Structure Documentation

## Overview
This document describes the AIXM (Aeronautical Information Exchange Model) query structure system used in the project. The system has evolved from an old structure to a new, more flexible structure that supports both AND and OR logical operations.

## File Structure
- **Location**: `Shared/DOST.Dataset/DatasetQueries/`
- **File Types**: 
  - `.single.json` - Queries for retrieving a single specific feature
  - `.list.json` - Queries for retrieving multiple features of a type

## New Query Structure (Current)

### Basic Structure
```json
{
  "featureType": "FeatureTypeName",
  "isDirect": true/false,
  "mandatoryLinks": ["LinkType1", "LinkType2"] | null,
  "children": [child_objects] | null,
  "filter": filter_object | null,
  "projection": projection_object | null
}
```

### Filter Structure
```json
{
  "filterType": "Binary",
  "logicOpType": "And" | "Or",
  "filters": [
    {
      "filterType": "Comparison",
      "comparisonOpType": "EqualTo" | "In" | "NotEqualTo" | etc.,
      "filterValue": "value" | { "values": ["val1", "val2"] },
      "propName": "PropertyName"
    }
  ]
}
```

### Projection Structure
```json
{
  "projectionType": "Included" | "Excluded",
  "properties": ["Property1", "Property2", "Property\\NestedProperty"]
}
```

## Key Concepts

### 1. Feature Types
- **AirportHeliport**: Main airport/heliport features
- **Runway**: Runway features
- **Taxiway**: Taxiway features
- **Airspace**: Airspace features
- **Navaid**: Navigation aid features
- **DesignatedPoint**: Waypoint features
- And many more...

### 2. isDirect Property
- `true`: Direct feature query (no mandatory links required)
- `false`: Indirect feature query (requires mandatory links to parent features)

### 3. mandatoryLinks
- Array of link types required to connect to parent features
- Examples: `["AssociatedAirportHeliport"]`, `["UsedRunway"]`, `["Fix"]`
- `null` when no links are required

### 4. Children Structure
- Hierarchical feature relationships
- Each child has the same structure as the parent
- Supports unlimited nesting levels
- Example: AirportHeliport → Runway → RunwayDirection → RunwayProtectArea

### 5. Filter Logic
- **Binary Filter**: Combines multiple conditions with AND/OR logic
- **Comparison Filter**: Individual property comparisons
- **Supported Operators**:
  - `EqualTo`: Exact match
  - `In`: Value must be in provided array
  - `NotEqualTo`: Not equal to value
  - And others...

### 6. Projection Logic
- **Included**: Only return specified properties
- **Excluded**: Return all properties except specified ones
- **Property Paths**: Use `\\` for nested properties (e.g., `"Annotation\\TranslatedNote\\Note"`)

## Common Patterns

### Single vs List Queries
- **`.single.json`**: Typically has 2 filter conditions
  1. Type filter (e.g., `Type = "RWY"`)
  2. Identifier filter (e.g., `Identifier = "{SubjectIdentifier}"`)
- **`.list.json`**: Typically has 1 filter condition
  1. Type filter only (e.g., `Type = "RWY"`)

### Complex Nested Structures
Example hierarchy for runway queries:
```
AirportHeliport
└── Runway
    ├── RunwayElement
    ├── RunwayMarking
    └── RunwayDirection
        ├── RunwayProtectArea
        │   └── RunwayProtectAreaLightSystem
        ├── RunwayCentrelinePoint
        │   └── GuidanceLine
        │       └── GuidanceLineMarking
        ├── ArrestingGear
        ├── VisualGlideSlopeIndicator
        ├── RunwayBlastPad
        ├── RunwayDirectionLightSystem
        └── ApproachLightingSystem
```

## Migration from Old Structure

### Old Structure (Deprecated)
```json
{
  "filterProperties": [
    {
      "filterType": "Comparison",
      "comparisonOpType": "EqualTo",
      "filterValue": "value",
      "propName": "PropertyName"
    }
  ],
  "checkedProperties": ["Property1", "Property2"]
}
```

### New Structure (Current)
```json
{
  "filter": {
    "filterType": "Binary",
    "logicOpType": "And",
    "filters": [
      {
        "filterType": "Comparison",
        "comparisonOpType": "EqualTo",
        "filterValue": "value",
        "propName": "PropertyName"
      }
    ]
  },
  "projection": {
    "projectionType": "Included",
    "properties": ["Property1", "Property2"]
  }
}
```

## Special Values and Placeholders

### Common Placeholders
- `{SubjectIdentifier}`: ID of the specific feature being queried
- `{AirportIdentifier}`: ID of the airport/heliport
- `{AirportHeliportIdentifier}`: Alternative airport identifier

### Common Filter Values
- **Runway Types**: `"RWY"`, `"NORMAL"`, `"DISPLACED"`, `"SHOULDER"`
- **Airspace Types**: `"FIR"`, `"UIR"`, `"TMA"`, `"CTA"`, `"CTR"`, `"CTR_P"`
- **Special Activity Types**: `"ADIZ"`, `"P"`, `"R"`, `"D"`, `"MTR"`, `"TSA"`

## File Examples

### Simple Query (gnss.single.json)
```json
{
  "featureType": "SpecialNavigationSystem",
  "isDirect": true,
  "mandatoryLinks": null,
  "children": null,
  "filter": {
    "filterType": "Binary",
    "logicOpType": "And",
    "filters": [
      {
        "filterType": "Comparison",
        "comparisonOpType": "EqualTo",
        "filterValue": "{SubjectIdentifier}",
        "propName": "Identifier"
      }
    ]
  },
  "projection": {
    "projectionType": "Included",
    "properties": ["ResponsibleOrganisation", "Name", "Annotation"]
  }
}
```

### Complex Nested Query (runway-directions.single.json)
- 4+ levels of nesting
- Multiple filter conditions with AND, OR logic
- Complex property projections
- Multiple child feature types

## Best Practices

1. **Consistency**: Always use the same structure pattern across similar features
2. **Null Handling**: Use `null` for optional filter/projection when not needed
3. **Property Paths**: Use `\\` for nested property access
4. **Filter Logic**: Use AND logic for multiple conditions (OR can be used for complex scenarios)
5. **Naming**: Follow the established naming conventions for file types

