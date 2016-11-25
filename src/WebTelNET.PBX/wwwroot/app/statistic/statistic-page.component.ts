import { Component, OnInit } from '@angular/core';

//import { CharacterService } from '../shared/character.service';

@Component({
    moduleId: module.id,
    templateUrl: 'statistic-page.html'
})
export class StatisticPageComponent implements OnInit {
    characters: string[];

    //constructor(private characterService: CharacterService) { }

    ngOnInit() {
        //this.characterService.getCharacters()
            //.subscribe(characters => this.characters = characters);
    }
}