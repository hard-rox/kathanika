# Plan: Split copilot-instructions.md into 4 Skills

Split the monolithic `.github/copilot-instructions.md` into 4 focused on-demand skills under `.github/skills/`, and slim down the instructions file to a lean architecture reference. Skills load only when relevant, reducing context bloat.

---

## Phase 1 — Create 4 Skill Files *(all 4 can be created in parallel)*

**1. `.github/skills/kathanika-dotnet/SKILL.md`**
- Trigger description: *"Use for .NET/C# development: CQRS commands/queries, DDD domain aggregates, FluentValidation, MediatR, xUnit tests, backend build/test commands, DependencyInjector registration."*
- Content extracted from instructions: CQRS Commands/Queries structure, Domain Aggregates (sealed classes, factory methods), Event Sourcing & Domain Events, FluentValidation, backend xUnit testing requirements, `dotnet build/test/format` commands, critical files: `DependencyInjector.cs`, `Program.cs`; pitfalls: domain logic in resolvers, missing validators, mutable aggregates

**2. `.github/skills/kathanika-angular/SKILL.md`**
- Trigger description: *"Use for Angular development: feature-based structure, kn-ui library, Jest testing, Apollo GraphQL codegen, frontend build/dev commands, component scaffolding."*
- Content extracted: Angular project structure (kn-ui vs ils-web), feature-based org, Jest testing (NOT Karma), spec file colocation rules, Apollo Angular + codegen workflow, `npm run codegen/build/test/start` commands, critical files: `app.routes.ts`, `codegen.ts`; pitfalls: `any` types, missing codegen run

**3. `.github/skills/kathanika-mongodb/SKILL.md`**
- Trigger description: *"Use for MongoDB/persistence work: repository pattern, Infrastructure.Persistence conventions, aggregate MongoDB requirements (sealed + private parameterless constructor), data access."*
- Content extracted: Repository pattern (define `IRepository<T>` in Domain, implement in `Infrastructure.Persistence`), MongoDB-specific aggregate requirement (private parameterless constructor), `Infrastructure.Persistence` structure; pitfall: mutable aggregates break MongoDB deserialization

**4. `.github/skills/kathanika-ui-ux/SKILL.md`**
- Trigger description: *"Use for UI/UX: kn-ui component library (alert, badge, chip, pagination, panel, searchbar), Tailwind CSS, design system rules, accessibility, responsive layout, animation/interaction patterns."*
- Content: kn-ui component inventory, Tailwind CSS usage, `kn-ui-tw-base.css` base styles, design system rules (spacing, color tokens, a11y), responsive layout patterns, animation/interaction patterns, reference to `MISSING_COMPONENTS_GUIDE.md`

---

## Phase 2 — Slim Down `copilot-instructions.md` *(depends on Phase 1)*

**5.** Rewrite `.github/copilot-instructions.md` to keep only:
- Kathanika intro + layered architecture overview (all layers listed)
- Monorepo structure (one-liner each for .NET and npm)
- Shared conventions: commit message format (`type(scope): desc`)
- CI/CD pipeline overview (linting → build → test → Sonar)
- Documentation links (`docs/architecture.md`, `CONTRIBUTING.md`, etc.)
- A "Skills" section listing the 4 new skill paths with one-line summaries

---

## Relevant Files

| File | Action |
|------|--------|
| `.github/copilot-instructions.md` | Rewrite to lean overview |
| `.github/skills/kathanika-dotnet/SKILL.md` | Create new |
| `.github/skills/kathanika-angular/SKILL.md` | Create new |
| `.github/skills/kathanika-mongodb/SKILL.md` | Create new |
| `.github/skills/kathanika-ui-ux/SKILL.md` | Create new |
| `src/app-projects/kn-ui/MISSING_COMPONENTS_GUIDE.md` | Referenced in UI skill |

---

## Verification

1. Each `SKILL.md` has valid YAML frontmatter: `name` matches folder name, `description` includes domain trigger keywords
2. Grep `copilot-instructions.md` — should contain no CQRS/aggregate/Jest/MongoDB implementation details after rewrite
3. All patterns from original file map to exactly one skill (no orphaned content, no duplication)
4. Slash commands: type `/kathanika` in Copilot Chat to verify 4 skills appear

---

## Decisions

- MongoDB skill = persistence only; GraphQL/HotChocolate resolver patterns stay in the dotnet skill
- UI/UX skill = kn-ui components + Tailwind + full design system (accessibility, spacing, color tokens, responsive, animation/interaction)
- `copilot-instructions.md` → lean overview only; no content duplication with skills
- `aiguideline.md` (referenced but missing) — out of scope for this plan
