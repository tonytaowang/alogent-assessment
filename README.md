# Alogent Digital Code Assessment

## Application Description

The following application is a system that allows users to post small notes on their boards.
For this exercise, added and removed data needs to only last as long as the web host is online.

The total set of deliverables as outlined would likely take 3 or 4 hours.
Only spend one hour on the assessment and get done what you can.
You will not be judged on the amount of code completed, only on the quality of the code turned in.
A project that doesn't compile or that fails any unit tests will not be accepted.

There is an initial "seed" of data that is stored in a `json` file. 
You can use this file to be your data store or you can store everything in-memory for the purposes of this assignment.
The file is deleted and restored at each run of the application.

This should run without anything other than Visual Studio or IIS.

## Getting Started

* Fork this repository on Github
* Clone your copy of the repository 

```
> git clone https://github.com/<username>/alogent-assessment.git
```

* Install NPM modules

```
> cd Src/Assessment.Client/ClientApp
> npm install
```

## Models

### Board

* Contain a name
* Contain the date is was created
* Contain any number of post-it notes

### Post-It

* Contain a title
* Contain some note information (URL, text, etc.)
* Contain the date it was created

## Target Deliverables 

1. Add a collection of "post-its" to the `Board` class
2. Add the following Web API endpoints

  - `POST /boards`

  creates a new board and returns at least the new id

  - `DELETE /boards/[board-id]`

  deletes an existing board and all related pins

  - `GET /boards/[board-id]/post-its/`

  gets all the pins attached to an existing board

  - `POST /boards/[board-id]/post-its/`

  adds a pin to an existing board

  - `DELETE /boards/[board-id]/post-its/[post-it-id]`

  removes a pin from a board

3. Relevant unit tests for server-side code

  - Code coverage - `95%`
      - For all logic areas added
      - Code already existing in the repository will not count against you
 
4. Update the front-end to use the new API endpoints

  - Create a new board
  - Delete a board
  
  - List all "post-its" for a board
  - Add a "post-it" to an existing board
  - Delete a "post-it"

## Tech Stack

* Git
* C#
* ASP.NET Core Web API
* Angular 5
* Bootstrap
* NUnit
* Moq

## Recommended Tools

* PowerShell
* VS Code or Visual Studio
 
## Out of scope

- User authenticiation/authorization
- Permanent data storage
- Design of front-end 
    - use the existing layout/design
    - if you have extra time and want to spend it changing the design, you are more than welcome