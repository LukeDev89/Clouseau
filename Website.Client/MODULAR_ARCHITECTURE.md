# Website.Client Modular Architecture

## Overview
The Website.Client has been refactored to follow a modular and maintainable architecture pattern. This refactoring improves separation of concerns, reusability, and testability while preserving all existing functionality.

## Architecture Pattern

### 1. Page Orchestrators
- **Location**: Main `.razor` files (e.g., `ProjectTasks.razor`, `Projects.razor`)
- **Responsibility**: 
  - State management and data loading
  - Service injection and business logic
  - Navigation and high-level event coordination
  - Modal and dialog management

### 2. Form Components
- **Location**: `Components/*Form.razor` files
- **Responsibility**:
  - Reusable form rendering for create/edit operations
  - Form validation and input handling
  - Event emission for save/cancel actions
  - No direct service calls or business logic

### 3. Grid Components  
- **Location**: `Components/*Grid.razor` files
- **Responsibility**:
  - Pure data display with sorting/filtering/paging
  - Event emission for user actions (edit, delete, etc.)
  - No business logic or service calls
  - Refreshable interface for external data updates

## Refactored Modules

### ProjectTasks Module
- **Page**: `ProjectTasks.razor` - Orchestrates all functionality
- **Form**: `Components/ProjectTaskForm.razor` - Create/edit task forms
- **Grid**: `Components/ProjectTaskGrid.razor` - Task data display
- **Legacy**: `ProjectTasksGrid.razor` - Maintained for compatibility

### Projects Module  
- **Page**: `Projects.razor` - Orchestrates CECO management
- **Form**: `Components/ProjectForm.razor` - Create/edit CECO forms
- **Grid**: `Components/ProjectGrid.razor` - CECO data display
- **Legacy**: `ProjectsGrid.razor` - Maintained for compatibility

### Teams Module (Components Created)
- **Form**: `Components/TeamForm.razor` - Team creation/editing
- **Grid**: `Components/TeamGrid.razor` - Team data display with user management

### Improvements Module (Components Created)
- **Form**: `Components/ImprovementForm.razor` - Improvement area forms
- **Grid**: `Components/ImprovementGrid.razor` - Improvement data display

## Communication Pattern

### Event-Based Communication
Components communicate through EventCallback parameters:

```razor
// Form Component Events
[Parameter] public EventCallback<TEntity> OnSaveEntity { get; set; }
[Parameter] public EventCallback OnCancelEntity { get; set; }

// Grid Component Events  
[Parameter] public EventCallback<TEntity> OnEditEntity { get; set; }
[Parameter] public EventCallback<TEntity> OnDeleteEntity { get; set; }
```

### Example Implementation
```razor
// Page Orchestrator
<EntityForm 
    Entity="@_newEntity"
    OnSaveEntity="HandleSaveNewEntity"
    OnCancelEntity="HandleCancelNewEntity" />

<EntityGrid 
    Entities="@_entities"
    OnEditEntity="HandleEditEntity"
    OnDeleteEntity="HandleDeleteEntity" />
```

## Benefits

### 1. Maintainability
- Clear separation of concerns
- Smaller, focused components
- Easier to understand and modify

### 2. Reusability
- Form components can be reused for create/edit scenarios
- Grid components are reusable across different contexts
- Common patterns can be extracted to shared components

### 3. Testability
- Components have well-defined inputs/outputs
- Business logic centralized in page orchestrators
- UI components can be tested independently

### 4. Scalability
- New features can be added as new components
- Existing components can be enhanced without affecting others
- Pattern can be applied to new modules consistently

## Migration Guide

### For Existing Modules
1. Create `Components/` subfolder
2. Extract form logic to `*Form.razor` component
3. Extract grid logic to `*Grid.razor` component  
4. Refactor main page to orchestrate components
5. Maintain legacy grid for backward compatibility

### For New Modules
1. Follow the established pattern from existing refactored modules
2. Create form and grid components first
3. Build page orchestrator to coordinate components
4. Implement event-based communication

## Legacy Compatibility
- Original grid components are maintained as legacy wrappers
- Existing functionality preserved during transition
- Progressive refactoring allows gradual adoption

## Next Steps
1. Complete page orchestrators for Teams and Improvements modules
2. Apply pattern to remaining modules (Users, TaskTypes, etc.)
3. Create shared base components for common patterns
4. Add comprehensive testing for new components