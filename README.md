# Content Factory Command Line Interface

Content Factory is a cross-platform command line tool for building game content. 

## Under development

You're looking at a very early version of this tool and it's under active development. The following README describes the basic concepts but the exact details are likely to change. Please keep this in mind if you decide to use it.

## Motivation

A lot of the raw game content you find [on the internet](https://www.gamedevmarket.net/?ally=yJRl98tX) or create yourself is not "game ready". Often you'll want to process the content files into a more efficent format or add extra metadata before loading them into your game.

Ideally, when you edit or add more content to your game this process should be easy and reproducible. 

## Basic usage

The simplest way to use the tool is to run the following command from your content directory.

```
content build
```

This command will look for a `content.json` file and build the content according to the tasks defined.

## How it works

A typical `content.json` file might look something like this:

```json
{
    "title": "2D PICKUPS PACK",
    "description": "It's FREE :) What else is there to say?",
    "author": "Ravenmore",
    "website": "https://ravenmore.itch.io/2d-pickups-pack",
    "tasks": [
        {
            "type": "pack",
            "parameters": {
                "sourceDirectory": "cake_64"
            }
        },
        {
            "type": "pack",
            "parameters": {
                "sourceDirectory": "cake_128"
            }
        }
    ]
}
```

When you run `content build` it will look at the tasks defined in this file and run in sequence. 

In this example it will run the texture packer on each source directory and output 2 packed textures and 2 data data files.
