# .github/prompts/README.md

This folder contains **prompt definitions** used to guide AI behavior in this repository.

Prompts define **what AI should do**, not how workflows are executed.

They are the **source of truth for instructions**, especially for:

* code generation
* code review
* validation tasks

---

## Purpose

Prompts exist to:

* standardize AI instructions across the repository
* enforce architecture and patterns through clear constraints
* reduce inconsistent or low-quality generated code

---

## Types of Prompts

### 1. Generation Prompts

Used to generate code following repository rules.

Example:

* generate controller
* generate service

---

### 2. Verification Prompts (Critical)

Used to evaluate code against repository standards.

Example:

* verify-architecture.md

These are **policy prompts**:

* they define pass/fail criteria
* they must not include workflow logic
* they must not be duplicated elsewhere

---

## Rules

* Prompts define **instructions and standards only**
* Prompts must not contain orchestration logic
* Prompts must not duplicate each other
* Prompts must align with:

  * docs/architecture.md
  * docs/patterns.md

---

## Relationship to Other Folders

* `.github/prompts/` → defines WHAT AI should do
* `.github/skills/` → defines HOW to perform a specific task
* `.github/agents/` → defines HOW to orchestrate tasks

---

## Key Principle

Prompts are the **single source of truth for instructions**.

If rules change, update prompts — not agents or skills.
