# Group Project – Git Setup Instructions
**CMPT 461 – Group 11**

Follow these steps to get the project on your computer and ready to work on.

---

## ⚠️ Important – Do NOT Download as ZIP

Do **not** use GitHub's "Download ZIP" button. The project uses Git LFS (Large File Storage) for large Unity assets. ZIP downloads skip LFS entirely and replace real files with tiny broken placeholders — Unity will fail to import textures, models, and audio with errors like `File could not be read`. Always use **GitHub Desktop or `git clone`** as described below.

---

## Step 1 – Install Required Tools

### Git
Download and install Git from [git-scm.com](https://git-scm.com)

### GitHub Desktop
Download and install from [desktop.github.com](https://desktop.github.com)
This is how you'll sync your changes without using the command line.

### Git LFS (Large File Storage)
The project uses Git LFS to handle large Unity files. You **must** install this or the project won't work correctly.

**On Mac (Apple Silicon or Intel):** Open Terminal and run:
```bash
brew install git-lfs
git lfs install
```

If you don't have Homebrew, install it first:
```bash
/bin/bash -c "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/HEAD/install.sh)"
```

**On Windows:** Download the installer from [git-lfs.github.com](https://git-lfs.github.com), run it, then open Command Prompt and run:
```bash
git lfs install
```

---

## Step 2 – Accept the Repository Invitation

Check your email for a GitHub collaboration invite from **nstjean05**. Click the link to accept it before trying to clone.

---

## Step 3 – Clone the Repository

1. Open **GitHub Desktop**
2. Click **File → Clone Repository**
3. Click the **GitHub.com** tab — the shared repo should appear in your list
4. Choose a location on your computer to save it
5. Click **Clone**

> GitHub Desktop will automatically fetch all LFS files during cloning. This is why cloning is required instead of using a ZIP download.

---

## Step 4 – Open the Project in Unity

1. Open **Unity Hub**
2. Click **Add → Add project from disk**
3. Navigate to the folder where you cloned the repo and select it
4. Open the project — Unity will reimport everything automatically *(this may take a few minutes the first time)*

---

## Daily Workflow

**Before you start working:**
- Open GitHub Desktop
- Click **Fetch origin** then **Pull** to get the latest changes from the team

**After you finish working:**
- Open GitHub Desktop
- You'll see a list of files you changed
- Write a short summary of what you did in the **Summary** box (e.g. `Added planet textures`)
- Click **Commit to main**
- Click **Push origin** to upload your changes

---

## Important Rules

- **Always pull before you start working** — this prevents conflicts
- **Never edit the same scene file as someone else at the same time** — Unity scene files are hard to merge. Coordinate with the team before opening a scene
- **Commit often** — small frequent commits are easier to manage than one giant one
- **If you see a conflict, let Noah know** before trying to resolve it yourself

---

## Need Help?

Contact **Noah** if anything isn't working.
