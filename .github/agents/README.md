# .github/agents/README.md

This folder contains **agents** used to orchestrate AI workflows.

An agent is responsible for:

* coordinating tasks
* applying prompts
* using skills
* producing structured results

---

## Purpose

Agents exist to:

* automate multi-step workflows
* apply repository rules consistently
* reduce manual coordination across tasks

---

## What an Agent Does

An agent:

1. understands the task
2. loads relevant context
3. applies prompts (rules)
4. uses skills (execution)
5. produces structured output

---

## What an Agent Does NOT Do

An agent does not:

* define architecture rules
* duplicate prompt logic
* replace human judgment
* invent new patterns

---

## Rules

* Agents must use prompts as the **source of truth**
* Agents must not duplicate verification rules
* Agents must remain thin (orchestration only)
* Agents must follow repository documents:

  * docs/architecture.md
  * docs/patterns.md

---

## Relationship to Other Folders

* `.github/prompts/` → defines WHAT to do
* `.github/skills/` → defines HOW to do it
* `.github/agents/` → coordinates execution

---

## Key Principle

Agents orchestrate.

They do not define policy.

They do not redefine rules.

They apply what already exists.
