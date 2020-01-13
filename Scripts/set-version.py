import re
import os

abspath = os.path.abspath(__file__)
dname = os.path.dirname(abspath)
parentdir = os.path.abspath(os.path.join(dname, os.pardir))

buildId = os.getenv('TRAVIS_BUILD_NUMBER', 0)
version = "1.0.0.{}".format(buildId)

print("Setting version: {}".format(version))

def setVersion(fileName):
  filePath = os.path.join(parentdir, fileName)
  with open (filePath, 'r' ) as f:
    content = f.read()
    content_new = re.sub('(?<=<Version>).*(?=<\/Version>)', version, content, flags = re.M)
    print(content_new)
  with open (filePath, 'w') as f:
    f.write(content_new)

setVersion('DeveThuisbezorgdBot/DeveThuisbezorgdBot.csproj')
setVersion('DeveThuisbezorgdBot.TelegramBot/DeveThuisbezorgdBot.TelegramBot.csproj')
setVersion('DeveThuisbezorgdBot.WebApp/DeveThuisbezorgdBot.WebApp.csproj')