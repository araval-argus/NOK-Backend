import { Component } from '@angular/core';

@Component({
  selector: 'app-add-nugget',
  standalone: true,
  imports: [],
  templateUrl: './add-nugget.component.html',
  styleUrl: './add-nugget.component.css'
})
export class AddNuggetComponent {
  title: string = 'BILEE E';
  subTitle: string = 'Return';
  subCategory: string = '#Keyword 3';
  format: string = 'Reject';
  region: string = 'Sub Category';
  language: string = 'Language *';
  upload: string = 'Upload';
  approvePublish: string = 'Approve / Publish';
  close: string = 'Close';
  createBy: string = 'Jack McDonald - System Admin';
  submittedBy: string = 'Jack McDonald - System Admin';
  approvedBy: string = 'Julia Simson - Reviewer';
 
  constructor() { }
 
  ngOnInit(): void {
  }
 }
