# CountingWordBlazor 

A .NET project that counts how many times words appear in a list — but only if they're found in a Trie.

Includes full test coverage and Docker support for easy development and CI integration.

---

##  What It Does

`CountingWordBlazor` features a `TrieWordCounter` that:

- Inserts **unique** words into a `Trie`
- Counts only words that return `true` from `Search`
- Ignores case (e.g., `azure` and `Azure` are the same)

---

##  Run with Docker

 **Step 1: Build the Image**


`docker build -t countingwordblazor .`


 **Step 2: Run the Container**

`docker run --rm countingwordblazor`


##  Run Tests Locally

Make sure you have the .NET SDK installed, then run:

```bash
dotnet test

---

