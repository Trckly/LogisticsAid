import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Trip } from '../models/trip.model';

@Injectable({
  providedIn: 'root',
})
export class TripService {
  url: string = environment.apiBaseUrl + '/Trip';

  constructor(private http: HttpClient) {}

  addTrip(trip: Trip) {
    console.log(trip);
    return this.http.post(this.url + '/AddTrip', trip, {
      withCredentials: true,
    });
  }
}
