# .github/skills/README.md

This folder contains **skills** used in AI-assisted development.

A skill is a **focused, reusable task definition**.

It defines **how to perform one specific task correctly**.

---

## Purpose

Skills exist to:

* standardize common development actions
* reduce repeated prompt writing
* ensure consistent outputs across developers

---

## What a Skill Is

A skill:

* is narrow in scope
* performs one job well
* follows repository architecture and patterns
* does not define repository-wide policy

Example:

* create-thin-controller
* validate-dto-boundary

---

## What a Skill Is NOT

A skill is not:

* a full workflow
* a code review system
* a replacement for architecture rules
* a place to redefine repository policy

---

## Rules

* Skills must be **specific and focused**
* Skills must not duplicate:

  * architecture rules
  * verification logic
* Skills must align with:

  * docs/architecture.md
  * docs/patterns.md
* Skills must not introduce new patterns

---

## Relationship to Other Folders

* `.github/prompts/` → defines rules and instructions
* `.github/skills/` → defines reusable tasks
* `.github/agents/` → orchestrates tasks

---

## Key Principle

A skill answers:

> “How do I do this one task correctly?”

Not:

> “What are the rules of the system?”
