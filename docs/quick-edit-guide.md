# Quick edit guide

* `.github/prompts/verify-architecture.md`

  * policy only
  * pass/fail rules only
  * output format only

* `.github/skills/create-thin-controller.md`

  * one focused generation capability
  * no repo-wide review logic
  * no architecture review duplication

* `.github/agents/architecture-pr-reviewer.md`

  * workflow only
  * loads docs
  * applies verifier
  * returns verifier output

Simple rule:

* prompt/verifier = standards
* skill = narrow task
* agent = orchestrator
