---
applyTo: "**"
---

# AI Development Instructions

## Architecture Rules

* Controllers must remain thin

* Business logic belongs in Services layer

* No direct DbContext usage in controllers

* Use DTOs for all external communication

## Repository AI Instructions

* Always follow existing Service → Repository pattern

* Do not introduce new architectural styles

* Reuse existing DTOs where possible

* Check existing controllers before generating new ones

* Maintain consistency with naming conventions

## AI Usage Guidelines

* AI-generated code must be reviewed before commit

* Do not accept code without understanding it

* Prefer explicit implementations over overly abstract AI suggestions

## Coding Standards

* Use async/await consistently

* Follow existing naming conventions

* Avoid duplication — refactor into shared services

## When NOT to use AI

* Critical business logic

* Security-sensitive code

* Complex architectural decisions
