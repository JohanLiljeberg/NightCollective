# 🎯 Membership Type System Implementation

## Overview

Added a flexible membership tier system to support three types of collective members:
1. **Full Member** - Complete profile with all games and contributions
2. **Subscribed Member** - Name, icon, position, and one featured game
3. **Unsubscribed Member** - Name and icon only (former members)

---

## 📁 New Files Created

### Models
- `Models/MembershipType.cs` - Enum defining three membership levels

### ViewComponents
- `ViewComponents/UnsubscribedMemberVC.cs` - Minimal member card component
- `ViewComponents/SubscribedMemberVC.cs` - Subscribed member with featured game
- `Views/Shared/Components/UnsubscribedMemberVC/Default.cshtml` - Unsubscribed view
- `Views/Shared/Components/SubscribedMemberVC/Default.cshtml` - Subscribed view

---

## 🔄 Modified Files

### Database Models
**`Models/CollectiveMember.cs`**
```csharp
// Added:
public MembershipType MembershipType { get; set; } = MembershipType.Full;
public int? FeaturedGameId { get; set; }
public Game? FeaturedGame { get; set; }
```

### ViewModels
**`ViewModels/CollectiveMemberViewModel.cs`**
```csharp
// Added:
public MembershipType MembershipType { get; init; } = MembershipType.Full;
public GameViewModel? FeaturedGame { get; init; }
```

**`ViewModels/CollectiveMemberFormViewModel.cs`**
```csharp
// Added:
public MembershipType MembershipType { get; set; } = MembershipType.Full;
public int? FeaturedGameId { get; set; }
```

### Services
**`Services/CollectiveService.cs`**
- Updated `MapMember()` to include MembershipType and FeaturedGame
- Updated `MapMemberForm()` to map new properties

### Repository
**`Repositories/SqlCollectiveRepository.cs`**
- Added `.Include(member => member.FeaturedGame)` to member queries

### Database Configuration
**`Data/AppDbContext.cs`**
```csharp
// Added FeaturedGame relationship:
entity.HasOne(member => member.FeaturedGame)
	.WithMany()
	.HasForeignKey(member => member.FeaturedGameId)
	.OnDelete(DeleteBehavior.SetNull);
```

### Views
**`Views/Home/Members.cshtml`**
- Added switch statement to render correct ViewComponent based on MembershipType

**`Views/Home/_MemberFields.cshtml`**
- Added membership type dropdown selector
- Added featured game selector for subscribed members
- Added JavaScript to show/hide fields based on selected membership type

---

## 🎨 UI/UX Features

### Unsubscribed Member Card
- Minimal design with gray/muted styling
- Shows only:
  - Small circular profile image (or icon placeholder)
  - Name
  - "Former member" label
- Compact card with light background
- Perfect for acknowledging past contributors

### Subscribed Member Card
- Medium-detail card with "Subscribed" badge
- Shows:
  - Profile image (card-img-top style)
  - Name
  - Position
  - Featured game (thumbnail + title + year)
- Blue badge to differentiate from full members
- Featured work section at bottom

### Full Member Card
- Existing rich CollectiveMemberVC
- Shows everything:
  - Full profile image (responsive)
  - Name, position, quote
  - All game contributions with involvement levels
  - Carousel for 6+ games

---

## 🔄 Admin Workflow

### Adding/Editing a Member

1. **Select Membership Type** from dropdown:
   - Full Member
   - Subscribed
   - Unsubscribed

2. **Form Fields Auto-Adjust**:

   **Full Member** shows:
   - ✅ Name
   - ✅ Membership Type
   - ✅ Image URL
   - ✅ Position
   - ✅ Quote
   - ✅ All Games (multi-select)

   **Subscribed** shows:
   - ✅ Name
   - ✅ Membership Type
   - ✅ Image URL
   - ✅ Position
   - ✅ Featured Game (single select)
   - ❌ Quote (hidden)
   - ❌ All Games (hidden)

   **Unsubscribed** shows:
   - ✅ Name
   - ✅ Membership Type
   - ✅ Image URL
   - ❌ Position (hidden)
   - ❌ Quote (hidden)
   - ❌ Featured Game (hidden)
   - ❌ All Games (hidden)

3. **JavaScript Validation**:
   - Fields are dynamically shown/hidden based on selection
   - No need to fill out irrelevant fields
   - Cleaner UX for admin

---

## 📊 Database Migration

**Migration Name**: `AddMembershipTypeAndFeaturedGame`

**Changes**:
```sql
-- Add MembershipType column (int, default 2 = Full)
ALTER TABLE CollectiveMembers ADD MembershipType INT NOT NULL DEFAULT 2;

-- Add FeaturedGameId foreign key (nullable)
ALTER TABLE CollectiveMembers ADD FeaturedGameId INT NULL;
ALTER TABLE CollectiveMembers ADD CONSTRAINT FK_CollectiveMembers_Games_FeaturedGameId 
	FOREIGN KEY (FeaturedGameId) REFERENCES Games(Id) ON DELETE SET NULL;
```

**To Apply**:
```bash
dotnet ef database update
```

---

## 🎯 Use Cases

### Use Case 1: Former Member
**Scenario**: Someone left the collective but you want to acknowledge them

**Setup**:
1. Create/edit member
2. Select "Unsubscribed"
3. Fill in: Name + Image URL
4. Save

**Result**: Small, subtle card showing they were part of the collective

---

### Use Case 2: New Contributor
**Scenario**: Someone just released their first game with the collective

**Setup**:
1. Create member
2. Select "Subscribed"
3. Fill in: Name, Image, Position, and select their game as Featured Game
4. Save

**Result**: Medium card highlighting their one game contribution

---

### Use Case 3: Core Member
**Scenario**: Long-time contributor with multiple games

**Setup**:
1. Create member
2. Select "Full Member" (default)
3. Fill in all fields including quote
4. Select all their games
5. Save

**Result**: Full rich card with carousel, quote, and all contributions

---

## 🧪 Testing Checklist

- [ ] Create an Unsubscribed member → Verify minimal card renders
- [ ] Create a Subscribed member with featured game → Verify game appears
- [ ] Create a Full member → Verify existing functionality still works
- [ ] Edit member from Full to Subscribed → Verify card updates
- [ ] Edit member from Subscribed to Unsubscribed → Verify card updates
- [ ] Delete a game that's someone's featured game → Verify FeaturedGameId sets to null
- [ ] Verify form fields show/hide correctly on membership type change
- [ ] Verify responsive images work on all three card types
- [ ] Run migration and check database schema

---

## 🔮 Future Enhancements

### Optional Improvements:
1. **Filtering** - Add tabs to filter members by type
2. **Badges** - Show membership level badge on all cards
3. **Transitions** - Animate member upgrades/downgrades
4. **Analytics** - Track member growth by type
5. **Bulk Actions** - Convert multiple members at once
6. **Email Notifications** - Alert members on status changes

---

## 🏗️ Architecture Notes

### Why Enum Over Separate Models?

✅ **Chosen Approach** (Single model + enum):
- ✅ One table, one repository
- ✅ Easy to upgrade/downgrade members
- ✅ Simple queries and migrations
- ✅ Consistent admin workflow
- ✅ Follows existing pattern (InvolvementLevel)

❌ **Alternative** (3 separate models):
- ❌ 3 tables, 3 repositories
- ❌ Complex member transitions
- ❌ Duplicate code across models
- ❌ Harder to maintain

### Database Relationship

**FeaturedGame**:
- Optional (nullable foreign key)
- `ON DELETE SET NULL` prevents cascade issues
- Only used by Subscribed members
- Properly eager-loaded with `.Include()`

---

## 📝 Code Quality

### Standards Followed:
- ✅ Consistent naming conventions
- ✅ Bootstrap 5 styling throughout
- ✅ Responsive design for all card types
- ✅ Proper EF Core relationship configuration
- ✅ Lazy loading for images
- ✅ Accessible HTML (ARIA labels)
- ✅ Form validation
- ✅ Clean separation of concerns

### Performance:
- Split queries for member loading
- Lazy image loading
- Minimal DOM for Unsubscribed cards
- Efficient conditional rendering

---

## 🚀 Deployment Notes

1. **Before Deploy**:
   ```bash
   dotnet build
   dotnet ef database update
   ```

2. **After Deploy**:
   - Review existing members (all defaulted to Full)
   - Update membership types as needed
   - Test admin form on production

3. **Rollback Plan**:
   ```bash
   dotnet ef database update <PreviousMigrationName>
   dotnet ef migrations remove
   ```

---

## 📚 Related Files

### Core Components:
- `Models/MembershipType.cs`
- `Models/CollectiveMember.cs`
- `ViewComponents/UnsubscribedMemberVC.cs`
- `ViewComponents/SubscribedMemberVC.cs`
- `ViewComponents/CollectiveMemberVC.cs` (existing)

### Views:
- `Views/Shared/Components/UnsubscribedMemberVC/Default.cshtml`
- `Views/Shared/Components/SubscribedMemberVC/Default.cshtml`
- `Views/Shared/Components/CollectiveMemberVC/Default.cshtml` (existing)
- `Views/Home/Members.cshtml`
- `Views/Home/_MemberFields.cshtml`

### Data Layer:
- `Data/AppDbContext.cs`
- `Repositories/SqlCollectiveRepository.cs`
- `Migrations/..._AddMembershipTypeAndFeaturedGame.cs`

---

**Status**: ✅ **Implementation Complete**  
**Build**: ✅ **Successful**  
**Migration**: ✅ **Generated**  
**Next Step**: Run `dotnet ef database update` and test! 🎉
