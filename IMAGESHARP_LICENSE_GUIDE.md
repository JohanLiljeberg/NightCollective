# 📄 SixLabors.ImageSharp Licensing Guide

## Executive Summary
`Services\ImageService.cs` depends on **SixLabors.ImageSharp** (currently `4.1.1`) for all image resizing/WebP generation. ImageSharp is **not** pure MIT/permissive — it uses a **dual license** model. Most small/non-commercial projects are covered for free, but Night Collective should confirm which tier applies once the app generates revenue or is run commercially, and know how to purchase a license if needed.

---

## 🧾 How ImageSharp Licensing Works

SixLabors dual-licenses ImageSharp:

1. **Apache License 2.0** (free) — applies automatically if your organization/project qualifies as:
   - An individual, non-profit, or open-source project, **or**
   - A business with **annual gross revenue below the SixLabors-published threshold** (check current figure on their site — this changes over time, historically in the low hundreds-of-thousands-of-dollars range).
2. **Commercial License** (paid) — required once your project/organization exceeds that revenue threshold, regardless of whether the software itself is sold. This is a **usage-based license**, not a per-seat/per-dev license.

> ⚠️ **Important:** SixLabors updates the exact revenue threshold and pricing periodically. Always check the live page before making a compliance decision — do not rely on cached figures.

---

## 🔍 Does Night Collective Need a Paid License?

Ask these questions:

| Question | If "Yes" |
|---|---|
| Is this a personal/hobby/community project with no revenue? | ✅ Free tier (Apache 2.0) applies |
| Is this run by a registered non-profit / community collective with no significant revenue? | ✅ Likely free tier — but confirm on SixLabors site |
| Does the organization operating this site have annual gross revenue **above** the published threshold? | ❌ Commercial license required |
| Are you unsure of your revenue classification? | 🔶 Contact SixLabors sales to confirm — better to ask than risk non-compliance |

For "Night Collective" as described (a community/game-collective site), you're almost certainly covered under the free Apache 2.0 tier today. Re-check this if the project starts generating meaningful revenue (e.g., sponsorships, paid memberships, ads).

---

## 🛒 How to Get/Download a Commercial License (if needed)

1. Go to **https://sixlabors.com/pricing/** in a browser (this page is JavaScript-rendered — it won't load via simple HTTP fetch tools, so open it directly).
2. Review the current tiers (they typically differ by team size / revenue bracket — e.g., "Startup", "Business", "Enterprise").
3. Select the tier matching your organization's size/revenue and click **Purchase** / **Get Started**.
4. Complete checkout via SixLabors' commercial portal (they use a third-party licensing platform — you'll receive a license key/agreement PDF by email).
5. **Store the license file/key** somewhere safe in your infra (e.g., a secrets vault or private config) — it is **not** a NuGet package setting, it's a legal/compliance artifact, not something referenced in code.
6. No code changes are required in `ImageService.cs` or `Night.csproj` — purchasing a commercial license doesn't change the package version or API; it only changes your legal right to use the existing Apache-licensed binaries commercially.

---

## 🔄 How This Interacts With NuGet Updates

- Updating the `SixLabors.ImageSharp` NuGet package version (like we just did: `4.0.0` → `4.1.1`) has **no effect** on licensing — the same dual-license terms apply to every version.
- Licensing is a **legal/business decision**, separate from the **technical dependency update** process described in the Image Service Health Check.
- If you upgrade to a future **major version** (e.g., 5.x), re-check the SixLabors licensing page since terms/thresholds can change between major releases.

---

## ✅ Action Checklist

- [ ] Confirm current annual revenue status of the organization operating Night Collective.
- [ ] Visit https://sixlabors.com/pricing/ to check current free-tier threshold and paid tier pricing.
- [ ] If under threshold: no action needed, continue using Apache 2.0 license.
- [ ] If at/above threshold: purchase appropriate commercial tier and archive the license confirmation.
- [ ] Revisit this check annually or whenever revenue status changes significantly.

---

## 📚 References
- SixLabors Pricing: https://sixlabors.com/pricing/
- ImageSharp GitHub (license file & FAQ): https://github.com/SixLabors/ImageSharp
