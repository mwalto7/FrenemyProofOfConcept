# Frenemy

```
cd into unity directory
git clone https://github.com/mwalto7/Frenemy.git
```

## How to Contribute

1. Checkout the branch you want to work on:

```
# List available branches:
#    Green: local branch
#    Red:   remote branch
$ git branch -a

# For local branches (green):
$ git checkout <branch-name>

# For remote branches (red):
$ git checkout --track <branch-name>

# To create a new local branch:
$ git checkout -b <branch-name>
```

2. Run `git status`.

3. Run `git pull`

4. When ready to add your changes to GitHub:

```
# Add your changes
$ git add .

# Commit your changes locally
$ git commit -m "Your reason for making this change."

# Finally, push your changes to GitHub
$ git push

# Or if it's your first time pushing
$ git push -u origin <branch-name>
```
