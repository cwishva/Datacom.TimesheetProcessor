<div id="top"></div>

<!-- PROJECT LOGO -->
<br />
<div align="center">
  <h3 align="center">Datacom Timesheet Processor</h3>

  <p align="center">
    An awesome start to Timesheet Processor!
    <br />
  </p>
</div>



<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#Datacom">API Improvemtns to Datacom</a></li>
  </ol>
</details>



<!-- ABOUT THE PROJECT -->
## About The Project

This is a simple console app that showcases a Timesheet Processor using Datacom API. The project is structured in a way we can swap any Front-end technology like MVC etc.

Here's why:
* Application, Domain, and Infrastructure are developed to show more details about the project as well as to help to swap any front-end app 
* Implement DRY principles for the rest of your life :smile:


<p align="right">(<a href="#top">back to top</a>)</p>



### Built With

Awesome frameworks/libraries were used to bootstrap this project.

* [Dotnet Core 6]([https://nextjs.org/](https://dotnet.microsoft.com/en-us/download/dotnet/6.0))
* [RestEase](https://reactjs.org/](https://www.nuget.org/packages/RestEase/))
* [Polly](https://vuejs.org/](https://www.nuget.org/packages/polly))

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- GETTING STARTED -->
## Getting Started

To get a local copy up and running follow these simple example steps.

### Prerequisites

Visual Studio, *Docker

### Installation

1. Clone the repo
   ```sh
   git clone https://github.com/cwishva/Datacom.TimesheetProcessor.git
   ```
2. Open Datacom.TimesheetProcessor.sln in Visual studio

4. Make sure appsettings.json has valid API configurations

4. Run the Console app Using Docker or VS
   

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- Datacom -->
## Datacom

API is looking great and the document was also written well to understand easily. Here is some feedback I think that would make the API better.

1. Pagination support. Although there is a Limit query the API does not come back with page size or pages back to the client. 
2. It is nice to have a filter to get by company code. had to pull all to filter. (Might not need in the context of business but for the current context I think it is good to have.)
3. It is nice to have a filter to "Payruns" to get with PayGroupIds. (Might not need in the context of business but for the current context I think it is good to have.)
4. It is good from the API documentation if there is a playground to interact with the API. 

<p align="right">(<a href="#top">back to top</a>)</p>

