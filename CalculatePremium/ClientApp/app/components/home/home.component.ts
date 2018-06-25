import { Component, Inject, Input } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'home',
    templateUrl: './home.component.html'
})
export class HomeComponent {
    public name: string;
    public dob: string;
    public gender: string;
    public premium: PremiumData;
    constructor(private http: Http, @Inject('BASE_URL') private baseUrl:string) { }

    public getData() {
        this.http.get('/api/SampleData/premium/'+[this.name,this.dob,this.gender].join('/')).subscribe(result => {
            this.premium = result.json() as PremiumData;
        }, error => console.error(error));
    }
}

interface PremiumData {
    Premium: string;    
}

