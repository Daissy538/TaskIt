export class Task {
  public id: string | undefined;
  public title: string | undefined;
  public description: string | undefined;
  public endDate: Date | undefined;

  constructor(id: string, title: string, description: string | undefined, endDate: Date | undefined) {
    this.id = id;
    this.title = title;
    this.description = description;
    this.endDate = endDate;
  }
}
