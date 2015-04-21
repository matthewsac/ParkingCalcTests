# ParkingCalcTests
Demonstration of C#/NUnit/Selenium framework using ParkingCalc web application

This repo contains a Visual Studio 2013 project I have created & have been working with to learn & explore test automation architecture using C#, NUnit, and Selenium. 

My previous experiences with Selenium have been using it with Python & Robot Framework, which though effective, using a framework out of the box robs you of the opportunity of learning how important architectural considerations are to your test automation efforts. One size does not fit all, and very often one technology doesn't often fit all.

The web is ripe with information about test automation architecture, C# programming, Selenium programming and so forth, but I saw very few places where working, well-documented code was fully available for exploration and execution. So I picked a very simple, publically accessible JS program that calcuates the cost of parking at a fictional airport (http://adam.goucher.ca/parkcalc), created a framework, and used NUnit Tests to subject the site to a full battery of automated tests.

CURRENT STATE

-Framework functionally complete
-Framwork reads element definitions from XML file
-Happy path tests are written in NUnit and read simple true/false assertions directly from the framework

IN PROGRESS
-Richer assertions against a result object containing lots of metadata
-Negative testing & error checking methods
-Adding SpecFlow to the mix to demonstrate test definitions in Gherkin that plug directly into the framework
