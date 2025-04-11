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

  getAllTrips() {
    return this.http.get(this.url + '/GetAllTrips', { withCredentials: true });
  }

  addTrip(trip: Trip) {
    return this.http.post(this.url + '/AddTrip', trip, {
      withCredentials: true,
    });
  }

  getTrips(pageIndex: number, pageSize: number) {
    return this.http.get(this.url + '/GetTrips', {
      withCredentials: true,
      params: { page: pageIndex.toString(), pageSize: pageSize.toString() },
    });
  }

  getRoutePointsById(ids: string[]) {
    return this.http.get(this.url + '/GetRoutePointsById', {
      withCredentials: true,
      params: { routePointIds: ids },
    });
  }

  deleteTrips(ids: string[]) {
    return this.http.delete(this.url + '/DeleteTrips', {
      withCredentials: true,
      params: {
        tripIds: ids,
      },
    });
  }
}
