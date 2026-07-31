# 🎨 Membership Card Types - Visual Reference

## Three Card Styles for Three Member Types

---

## 1️⃣ Unsubscribed Member Card (Minimal)

### Purpose
For former members or contributors you want to acknowledge without highlighting

### Visual Design
```
┌─────────────────────────┐
│                         │
│         👤 or 🖼️         │  ← Circular image/icon
│                         │
│     John Doe            │  ← Name (muted color)
│   Former member         │  ← Label
│                         │
└─────────────────────────┘
```

### Styling
- **Background**: Light gray (`bg-light`)
- **Size**: Compact card
- **Colors**: Muted text, low opacity
- **Image**: 80x80px circle, slightly transparent
- **Padding**: Minimal

### When to Use
- ✅ Past contributors who left
- ✅ One-time collaborators
- ✅ Historical acknowledgments
- ✅ Inactive members

### Example Data
```json
{
  "name": "Alex Rivera",
  "membershipType": "Unsubscribed",
  "imageSmallUrl": "/images/members/alex_sm.webp"
}
```

---

## 2️⃣ Subscribed Member Card (Medium)

### Purpose
For active contributors with one featured game

### Visual Design
```
┌─────────────────────────┐
│                         │
│    [Profile Image]      │  ← Full-width image
│                         │
└─────────────────────────┘
│ 🔵 Subscribed           │  ← Badge
│                         │
│ Sarah Chen              │  ← Name (bold)
│ Game Designer           │  ← Position
│                         │
│ ─────────────────────   │
│ Featured Work           │
│                         │
│ [🎮] Neon Dreams        │  ← Featured game
│      2024               │
│                         │
└─────────────────────────┘
```

### Styling
- **Background**: White card
- **Badge**: Blue primary color
- **Image**: Card-img-top, 200px height
- **Featured Game**: 64x64px thumbnail + details
- **Border**: Clean, minimal

### When to Use
- ✅ New members with one game
- ✅ Guest contributors
- ✅ Spotlight a specific project
- ✅ Mid-tier membership level

### Example Data
```json
{
  "name": "Sarah Chen",
  "position": "Game Designer",
  "membershipType": "Subscribed",
  "featuredGame": {
	"title": "Neon Dreams",
	"releaseYear": 2024,
	"imageSmallUrl": "/images/games/neon-dreams_sm.webp"
  }
}
```

---

## 3️⃣ Full Member Card (Rich)

### Purpose
Core collective members with complete portfolio

### Visual Design
```
┌─────────────────────────┐
│                         │
│   [Responsive Image]    │  ← Responsive picture element
│                         │
└─────────────────────────┘
│ Collective member       │  ← Eyebrow
│ Jordan Blake            │  ← Name (large)
│ Lead Developer          │  ← Position (accent color)
│                         │
│ "I believe games are    │  ← Quote (blockquote)
│  the art form of our    │
│  generation."           │
│                         │
│ ─────────────────────   │
│ Games (12)              │
│ 🥇🥈🥉🎮🎮             │  ← Game thumbnails
│ [● ● ○]                 │  ← Carousel if 6+
│                         │
└─────────────────────────┘
```

### Styling
- **Background**: White card
- **Image**: Responsive 4:3, lazy loading
- **Quote**: Special blockquote styling
- **Games**: Grid or carousel
- **Medals**: 🥇 Lead, 🥈 Major, 🥉 Supporting
- **Details**: Full profile

### When to Use
- ✅ Core collective members
- ✅ Long-term contributors
- ✅ Members with multiple games
- ✅ Featured portfolio showcase

### Example Data
```json
{
  "name": "Jordan Blake",
  "position": "Lead Developer",
  "quote": "I believe games are the art form of our generation.",
  "membershipType": "Full",
  "gameContributions": [
	{
	  "title": "Space Odyssey",
	  "involvementLevel": "Lead",
	  "workAreas": ["Code", "Design"]
	}
  ]
}
```

---

## 📊 Comparison Table

| Feature              | Unsubscribed | Subscribed | Full     |
|---------------------|--------------|------------|----------|
| **Profile Image**    | Circle (80px)| Card-top   | Responsive |
| **Name**             | ✅           | ✅         | ✅       |
| **Position**         | ❌           | ✅         | ✅       |
| **Quote**            | ❌           | ❌         | ✅       |
| **Featured Game**    | ❌           | ✅ (1)     | ❌       |
| **All Games**        | ❌           | ❌         | ✅ (All) |
| **Involvement Lvl**  | ❌           | ❌         | ✅       |
| **Badge**            | "Former"     | "Subscribed"| "Collective" |
| **Card Size**        | Small        | Medium     | Large    |
| **Background**       | Light gray   | White      | White    |

---

## 🎯 Mobile-First Responsive Behavior

### Grid Layout (Members.cshtml)
```razor
<div class="row g-4">
	@foreach (var member in Model.Members)
	{
		<div class="col">  <!-- Auto-sizing column -->
			@switch (member.MembershipType)
			{
				case Unsubscribed:
					@* Small card - can fit 3-4 per row *@
				case Subscribed:
					@* Medium card - 2-3 per row *@
				case Full:
					@* Large card - 1-2 per row *@
			}
		</div>
	}
</div>
```

### Breakpoints
- **Mobile** (< 576px): 1 card per row
- **Tablet** (≥ 768px): 2 cards per row
- **Desktop** (≥ 992px): 2-3 cards per row
- **Large** (≥ 1200px): 3-4 cards per row

---

## 🎨 Color Scheme

### Unsubscribed
- Text: `text-muted` (gray)
- Background: `bg-light` (light gray)
- Border: `border-secondary` with opacity
- Overall feel: Subtle, historical

### Subscribed
- Badge: `bg-primary bg-opacity-10 text-primary border border-primary`
- Text: Standard dark
- Background: White
- Accent: Blue primary color
- Overall feel: Active, highlighted

### Full
- Eyebrow: Accent color
- Title: Bold, large
- Position: `text-accent fw-semibold`
- Background: White
- Overall feel: Premium, detailed

---

## 📱 Accessibility

All three cards include:
- ✅ Semantic HTML (`<article>`)
- ✅ Proper heading hierarchy
- ✅ Alt text for images
- ✅ ARIA labels where needed
- ✅ Keyboard navigation support
- ✅ Focus indicators
- ✅ High contrast text

---

## 🧪 Example Rendering Code

```razor
@switch (member.MembershipType)
{
	case MembershipType.Unsubscribed:
		@await Component.InvokeAsync("UnsubscribedMemberVC", new { member })
		break;

	case MembershipType.Subscribed:
		@await Component.InvokeAsync("SubscribedMemberVC", new { member })
		break;

	case MembershipType.Full:
	default:
		@await Component.InvokeAsync("CollectiveMemberVC", new { member })
		break;
}
```

---

## 🎭 Real-World Scenarios

### Scenario A: Growing Collective
```
┌─────────────────────────────────────────────┐
│  Full Member    Full Member    Subscribed   │
│  (Founder)      (Core Dev)     (New Artist) │
│                                              │
│  Unsubscribed   Subscribed                  │
│  (Alumni)       (Guest)                     │
└─────────────────────────────────────────────┘
```

### Scenario B: Event Contributors
```
┌─────────────────────────────────────────────┐
│  Full Member × 3    (Core organizers)       │
│  Subscribed × 5     (This event's speakers) │
│  Unsubscribed × 2   (Past event helpers)    │
└─────────────────────────────────────────────┘
```

---

## 🔄 Transition Flow

```
New Contributor
	  ↓
Unsubscribed (Acknowledgment only)
	  ↓
Subscribed (Featured in one project)
	  ↓
Full Member (Multiple projects, full profile)
```

Or reverse for leaving members:
```
Full Member → Subscribed → Unsubscribed
```

---

**Visual Reference Complete!** 🎨  
These three card types give you maximum flexibility for showcasing your collective's diverse contributors.
