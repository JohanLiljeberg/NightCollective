# Admin System Documentation

## Overview
A simple session-based admin system for managing events, games, and collective members through a unified dashboard.

## Admin Access

### Login
- **URL**: `/Admin/Login`
- **Password**: Stored in `appsettings.json` under `Admin:Password`
- **Default Password**: `NightOwl2026!`

### Security Features
- Session-based authentication (2-hour timeout)
- HttpOnly cookies
- Anti-forgery token protection on all forms
- Authorization filter on all admin actions

## Admin Dashboard

### URL
`/Admin/Dashboard`

### Features
The dashboard has three tabs:

1. **📅 Create Event**
   - Add new events with images (file upload or URL)
   - Validation for required fields
   - Date validation (cannot be in the past)

2. **🎮 Add Game**
   - Add games with member contributions
   - Set involvement levels (Lead, Major, Supporting)
   - Select work areas for each member
   - Platform and genre selection

3. **👥 Add Member**
   - Add collective members
   - Assign them to existing games
   - Set position and quote

### Navigation
- Admin link appears in the top-right of the navbar (🔒 Admin)
- Clicking it redirects to login if not authenticated
- Success messages appear after creating items
- Easy navigation back to public pages

## Changing the Admin Password

Edit `appsettings.json`:

```json
"Admin": {
  "Password": "YourNewPassword123!"
}
```

**Important**: For production, consider using:
- Environment variables
- Azure Key Vault
- User Secrets (for development)

## Implementation Details

### Files Created
- `Filters/AdminAuthorizationFilter.cs` - Authorization filter
- `Controllers/AdminController.cs` - Admin controller
- `ViewModels/AdminDashboardViewModel.cs` - Dashboard view model
- `Views/Admin/Login.cshtml` - Login page
- `Views/Admin/Dashboard.cshtml` - Admin dashboard

### Files Modified
- `appsettings.json` - Added admin password
- `Program.cs` - Added session support and filter registration
- `Services/ICollectiveService.cs` - Added form methods
- `Services/CollectiveService.cs` - Implemented form methods
- `Views/Shared/_Layout.cshtml` - Added admin link

### Database Migration
- Created migration: `UpdateManyToManyRelationship`
- Fixes pending model changes for GameMemberContribution join table

## Usage

1. Navigate to your site
2. Click "🔒 Admin" in the navbar
3. Enter password: `NightOwl2026!`
4. Use the tabbed interface to create events, games, or members
5. Click "Logout" when done

## Notes

- Only one admin user (hardcoded password)
- Session expires after 2 hours of inactivity
- All forms reuse existing partial views for consistency
- Validation is performed both client-side and server-side
